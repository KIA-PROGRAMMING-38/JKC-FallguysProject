using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CommonHoop : MonoBehaviour
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

    private void Awake()
    {
        _goalCheck = transform.Find("GoalCheck").GetComponent<GoalCheck>();
        Debug.Assert(_goalCheck != null);
        _passEffect = transform.Find("PassEffect").GetComponent<ParticleSystem>();
        _passEffectReverse = transform.Find("PassEffectReverse").GetComponent<ParticleSystem>();
        _passEffect.Stop();
        _passEffectReverse.Stop();
        Debug.Assert(_passEffect != null);
        Debug.Assert(_passEffectReverse != null);
        
        _originPosition = transform.position;
        _currentHeight = _originPosition.y;
    }

    public void Initialize(HoopController hoopController)
    {
        _hoopController = hoopController;
    }

    private void Start()
    {
        _goalCheck.OnPlayerEnter += HandlePlayerEnter;
        _goalCheck.OnPlayerExit += HandlePlayerExit;
    }

    private void HandlePlayerEnter()
    {
        if (!_playerEntered && !_isMoving)
        {
            _playerEntered = true;
            ParticlePlay().Forget();
            PropellerAnimation().Forget();
            
            _hoopController.PlayerPassesHoop(1);
        }
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

    private async UniTaskVoid PropellerAnimation()
    {
        _isMoving = true;
        float targetHeight = _originPosition.y + _heightOffset;
        float elapsedTime = 0f;
        float moveSpeedMultiplier = _moveSpeed;
        while (_currentHeight < targetHeight)
        {
            elapsedTime += Time.deltaTime * moveSpeedMultiplier;
            _currentHeight = Mathf.Lerp(_originPosition.y, targetHeight, elapsedTime / _duration);
            transform.position = new Vector3(_originPosition.x, _currentHeight, _originPosition.z);

            await UniTask.Yield();
        }

        await UniTask.Delay(TimeSpan.FromSeconds(_duration));

        elapsedTime = 0f;
        while (_currentHeight > _originPosition.y)
        {
            elapsedTime += Time.deltaTime * moveSpeedMultiplier;
            _currentHeight = Mathf.Lerp(targetHeight, _originPosition.y, elapsedTime / _duration);
            transform.position = new Vector3(_originPosition.x, _currentHeight, _originPosition.z);
            
            await UniTask.Yield();
        }

        _isMoving = false;
    }

    private void OnDestroy()
    {
        _goalCheck.OnPlayerEnter -= HandlePlayerEnter;
        _goalCheck.OnPlayerExit -= HandlePlayerExit;
    }
}
