using System;
using System.Threading;
using LiteralRepository;
using Photon.Pun;
using UniRx;
using UnityEngine;
using Util.Helper;

public class LowerBar : MonoBehaviourPun
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration;

    [SerializeField] private float test;
    
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
        StageManager.Instance.StageDataManager.CurrentSequence
            .DistinctUntilChanged()
            .Skip(1)
            .Where(sequence => sequence == StageDataManager.StageSequence.GameCompletion)
            .Subscribe(_ => StopRigidbodyMovement())
            .AddTo(this);
    }

    private void TriggerStart()
    {
        double futureNetworkTime = PhotonTimeHelper.GetFutureNetworkTime(3);
        photonView.RPC("RpcInitiateRotation", RpcTarget.AllBuffered, futureNetworkTime);
    }

    [PunRPC]
    public void RpcInitiateRotation(double startNetworkTime)
    {
        PhotonTimeHelper.ScheduleDelayedAction(startNetworkTime, () => SetRotaition(), _cancellationTokenSource.Token);
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

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(TagLiteral.Player))
        {
            PhotonView playerPhotonView = col.gameObject.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                Rigidbody rigidbody = col.collider.GetComponent<Rigidbody>();
                
                Vector3 direction = col.transform.position - transform.position;
                direction = direction.normalized;

                rigidbody.AddForce(direction * test, ForceMode.Impulse);
            }
        }
    }
}
