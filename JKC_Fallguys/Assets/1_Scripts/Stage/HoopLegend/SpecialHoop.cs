using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

public class SpecialHoop : MonoBehaviourPun
{
    private HoopController _hoopController;
    private ParticleSystem _passEffect;
    private ParticleSystem _passEffectReverse;
    
    private GoalCheck _goalCheck;
    private Vector3 _originPosition;
    private bool _isMoving;
    private bool _playerEntered;
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _heightOffset;
    [SerializeField] private float _moveSpeed;
    private float _currentHeight;
    private CancellationTokenSource _cancelToken;

    private void Awake()
    {
        StageDontDestroyOnLoadSet();
        
        _goalCheck = transform.Find("GoalCheck").GetComponent<GoalCheck>();
        Debug.Assert(_goalCheck != null);
        _passEffect = transform.Find("PassEffect").GetComponent<ParticleSystem>();
        _passEffectReverse = transform.Find("PassEffectReverse").GetComponent<ParticleSystem>();
        _passEffect.Stop();
        _passEffectReverse.Stop();
        _cancelToken = new CancellationTokenSource();
        Debug.Assert(_passEffect != null);
        Debug.Assert(_passEffectReverse != null);
        
        _originPosition = transform.position;
        _currentHeight = _originPosition.y;
    }
    
    private void StageDontDestroyOnLoadSet()
    {
        DontDestroyOnLoad(gameObject);
        photonView.RPC("RpcSetParentStageRepository", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcSetParentStageRepository()
    {
        transform.SetParent(StageManager.Instance.ObjectRepository.transform);
    }

    private void Start()
    {
        _hoopController = StageManager.Instance.ObjectRepository.transform.Find("MapHoopLegend(Clone)").GetComponentInChildren<HoopController>();
        
        _goalCheck.OnPlayerEnter += HandlePlayerEnter;
        _goalCheck.OnPlayerExit += HandlePlayerExit;
    }

    private void HandlePlayerEnter()
    {
        if (!_playerEntered && !_isMoving)
        {
            _playerEntered = true;
            _hoopController.PlayerPassesHoop(2);
            
            photonView.RPC("RpcHandlePlayerEnter", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void RpcHandlePlayerEnter()
    {
        ParticlePlay().Forget();
        PropellerAnimation(_cancelToken).Forget();
    }
    
    private async UniTaskVoid ParticlePlay()
    {
        _passEffect.Play();
        _passEffectReverse.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        
        _passEffect.Stop();
        _passEffectReverse.Stop();
    }

    private void HandlePlayerExit()
    {
        _playerEntered = false;
    }

    private async UniTaskVoid PropellerAnimation(CancellationTokenSource cancelToken)
    {
        _isMoving = true;
        float targetHeight = _originPosition.y + _heightOffset;
        float elapsedTime = 0f;
        float moveSpeedMultiplier = _moveSpeed;
    
        while (_currentHeight < targetHeight)
        {
            // If cancellation is requested, exit the loop
            if (cancelToken.Token.IsCancellationRequested)
            {
                _isMoving = false;
                return;
            }

            elapsedTime += Time.deltaTime * moveSpeedMultiplier;
            _currentHeight = Mathf.Lerp(_originPosition.y, targetHeight, elapsedTime / _duration);
            transform.position = new Vector3(_originPosition.x, _currentHeight, _originPosition.z);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(_duration));

        elapsedTime = 0f;
        while (_currentHeight > _originPosition.y)
        {
            // If cancellation is requested, exit the loop
            if (cancelToken.Token.IsCancellationRequested)
            {
                _isMoving = false;
                return;
            }

            elapsedTime += Time.deltaTime * moveSpeedMultiplier;
            _currentHeight = Mathf.Lerp(targetHeight, _originPosition.y, elapsedTime / _duration);
            transform.position = new Vector3(_originPosition.x, _currentHeight, _originPosition.z);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        _isMoving = false;
    }

    
    private void OnDestroy()
    {
        _goalCheck.OnPlayerEnter -= HandlePlayerEnter;
        _goalCheck.OnPlayerExit -= HandlePlayerExit;
        
        _cancelToken.Cancel();
    }
}
