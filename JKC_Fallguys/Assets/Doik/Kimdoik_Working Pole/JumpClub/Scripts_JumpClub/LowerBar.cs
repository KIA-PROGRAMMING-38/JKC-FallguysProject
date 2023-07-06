using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UniRx;
using UnityEditor.Rendering;
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

    private async UniTaskVoid ObstacleRotation(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: cancelToken);

            _LowerBar.AddTorque(Vector3.up * rotationSpeed);
        }
    }
    
    [PunRPC]
    public void RpcInitiateRotation(long startUnixTimestamp)
    {
        StartRotationAtTimestamp(startUnixTimestamp).Forget();
    }

    private async UniTaskVoid StartRotationAtTimestamp(long startUnixTimestamp)
    {
        int waitTimeSeconds = (int)(startUnixTimestamp - DateTimeOffset.Now.ToUnixTimeSeconds());
        if (waitTimeSeconds > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitTimeSeconds));
        }
        
        IncreaseRotationSpeed(_cancellationTokenSource.Token).Forget();
        ObstacleRotation(_cancellationTokenSource.Token).Forget();
    }


    #pragma warning disable CS1998
    private async UniTaskVoid TriggerStart()
    #pragma warning restore CS1998
    {
        long startUnixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() + 5;

        photonView.RPC("RpcInitiateRotation", RpcTarget.AllBuffered, startUnixTimestamp);
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
