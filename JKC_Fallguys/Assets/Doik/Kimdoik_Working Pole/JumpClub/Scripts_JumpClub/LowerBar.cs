using System;
using System.Threading;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class LowerBar : MonoBehaviourPun
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration;
    
    private Rigidbody _rotatingObstacle;
    private CancellationTokenSource _cancellationTokenSource;
    
    private void Awake()
    {
        _rotatingObstacle = GetComponent<Rigidbody>();
        _cancellationTokenSource = new CancellationTokenSource();

        InitializeRx();
    }
    
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            TriggerStart();
        }
    }

    private void InitializeRx()
    {
        StageDataManager.Instance.IsGameActive
            .DistinctUntilChanged()
            .Skip(1)
            .Where(state => !state)
            .Subscribe(_ => StopRigidbodyMovement())
            .AddTo(this);
    }

    private void TriggerStart()
    {
        double futureNetworkTime = StageDataManager.Instance.PhotonTimeHelper.GetFutureNetworkTime(3);
        photonView.RPC("RpcInitiateRotation", RpcTarget.AllBuffered, futureNetworkTime);
    }

    [PunRPC]
    public void RpcInitiateRotation(double startNetworkTime)
    {
        StageDataManager.Instance.PhotonTimeHelper.ScheduleDelayedAction(startNetworkTime, () => SetRotaition(), _cancellationTokenSource.Token);
    }

    private void SetRotaition()
    {
        IObservable<long> rotationTask = Observable.EveryFixedUpdate()
            .Where(_ => !_cancellationTokenSource.IsCancellationRequested)
            .Do(_ => _rotatingObstacle.AddTorque(Vector3.up * rotationSpeed));

        IObservable<long> speedIncreaseTask = Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => !_cancellationTokenSource.IsCancellationRequested)
            .Do(_ => rotationSpeed += acceleration);

        rotationTask.Merge(speedIncreaseTask).Subscribe().AddTo(this);
    }

    private void StopRigidbodyMovement()
    {
        _cancellationTokenSource.Cancel();

        _rotatingObstacle.angularVelocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
