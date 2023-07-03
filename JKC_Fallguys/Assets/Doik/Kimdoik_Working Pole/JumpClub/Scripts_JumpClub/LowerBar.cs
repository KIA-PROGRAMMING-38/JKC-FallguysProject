using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class LowerBar : MonoBehaviourPun
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration;
    
    private Rigidbody _LowerBar;
    private CancellationTokenSource _cancellationTokenSource;
    
    private void Awake()
    {
        _LowerBar = GetComponent<Rigidbody>();
        _cancellationTokenSource = new CancellationTokenSource();

        InitializeRx();
    }
    
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            TriggerStart().Forget();
        }
    }

    private void InitializeRx()
    {
        StageDataManager.Instance.IsGameActive
            .DistinctUntilChanged()
            .Skip(1)
            .Where(state => !state)
            .Subscribe(_ => StopAction())
            .AddTo(this);
    }

    private void StopAction()
    {
        _cancellationTokenSource.Cancel();

        _LowerBar.angularVelocity = Vector3.zero;
    }

    private async UniTaskVoid TriggerStart()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        photonView.RPC("RpcInitiateRotation", RpcTarget.All);
    }

    [PunRPC]
    public void RpcInitiateRotation()
    {
        IncreaseRotationSpeed(_cancellationTokenSource.Token).Forget();
        ObstacleRotation(_cancellationTokenSource.Token).Forget();
    }
    
    private async UniTaskVoid ObstacleRotation(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: cancelToken);

            _LowerBar.AddTorque(Vector3.up * rotationSpeed);
        }
    }

    private async UniTaskVoid IncreaseRotationSpeed(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f),  cancellationToken: cancelToken);
            
            rotationSpeed += acceleration;
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
