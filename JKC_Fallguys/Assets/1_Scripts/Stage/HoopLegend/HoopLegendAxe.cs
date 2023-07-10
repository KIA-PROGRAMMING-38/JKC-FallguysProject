using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class HoopLegendAxe : MonoBehaviourPun
{
    [SerializeField] 
    private float _minRotationSpeed;
    [SerializeField] 
    private float _maxRotationSpeed;
    
    private float _axeRotationSpeed;

    private Transform _axeTransform;
    private CancellationTokenSource _cancellationToken;

    private void Awake()
    {
        StageDontDestroyOnLoadSet();
        
        _axeTransform = transform.Find("AxeBody").GetComponent<Transform>();
        _cancellationToken = new CancellationTokenSource();
        Debug.Assert(_axeTransform != null);
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
        if (PhotonNetwork.IsMasterClient)
        {
            _axeRotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
            photonView.RPC("RpcInitiateRotation", RpcTarget.All, _axeRotationSpeed);
        }
    }

    [PunRPC]
    public void RpcInitiateRotation(float axeRotationSpeed)
    {
        _axeRotationSpeed = axeRotationSpeed;
        
        AxeRotation(_cancellationToken).Forget();
    }

    private async UniTask AxeRotation(CancellationTokenSource cancelToken)
    {
        while (true)
        {
            _axeTransform.Rotate(Vector3.forward * _axeRotationSpeed * Time.deltaTime);

            await UniTask.Yield(PlayerLoopTiming.Update, cancelToken.Token);
        }
    }

    private void OnDestroy()
    {
        _cancellationToken.Cancel();
    }
}
