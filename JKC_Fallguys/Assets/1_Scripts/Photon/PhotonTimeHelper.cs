using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;

public class PhotonTimeHelper : MonoBehaviourPun
{
    public void Initialize()
    {
        // DontDestroyOnLoad(gameObject);
        photonView.RPC("RpcSetParentStageRepository", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcSetParentStageRepository()
    {
        // transform.SetParent(StageRepository.Instance.gameObject.transform);
        DontDestroyOnLoad(gameObject);
    }
    
    public static readonly int SyncIntervalMs = 100;
    
    private double _serverTime;
    public double ServerTime
    {
        get
        {
            return _serverTime;
        }
        set
        {
            _serverTime = value;
        }
    }
    
    public async UniTask SyncServerTime(CancellationToken cancellationToken)
    {
        while (true)
        {
            ServerTime = PhotonNetwork.Time;
            photonView.RPC("RpcSyncServerTime", RpcTarget.Others, _serverTime);
            await UniTask.Delay(TimeSpan.FromMilliseconds(SyncIntervalMs), cancellationToken: cancellationToken);
        }
    }
    
    [PunRPC]
    public void RpcSyncServerTime(double serverTime)
    {
        ServerTime = serverTime;
    }

    
    /// <summary>
    /// 지정한 지연 시간 후의 포톤 네트워크 시간(초 단위)을 반환하는 메서드입니다.
    /// 예를 들어, 현재 시간으로부터 5초 후의 포톤 네트워크 시간을 얻으려면 GetFutureNetworkTime(5)를 호출하면 됩니다.  
    /// </summary>
    /// <param name="delayTime">지연 시간입니다.</param>
    public double GetFutureNetworkTime(double delayTime)
    {
        double startNetworkTime = PhotonNetwork.Time + delayTime;
        return startNetworkTime;
    }
    
    /// <summary>
    /// 지정한 포톤 네트워크 시간이 될 때까지 대기한 후 액션을 실행하는 메서드입니다.
    /// 예를 들어, 현재 시간으로부터 5초 후에 특정 액션을 수행하려면
    /// 먼저 GetFutureNetworkTime(5)를 호출하여 5초 후의 포톤 네트워크 시간을 얻은 후,
    /// 그 시간과 액션을 ScheduleDelayedAction에 전달하면 됩니다.
    /// </summary>
    /// <param name="startNetworkTime">PhotonNetworkTimeHelper.GetFutureNetworkTime로 반환된 값을 인수로 넣어줍니다.</param>
    /// <param name="action">실행할 동작을 정의합니다.</param>
    public void ScheduleDelayedAction(double startNetworkTime, Action action, CancellationToken token)
    {
        ScheduleDelayUniTask(startNetworkTime, action, token).Forget();
    }

    // 내부적으로 ScheduleDelayedAction 메서드에서 사용되는 비동기 메서드입니다.
    // 이 메서드는 포톤 네트워크 시간이 될 때까지 대기하고, 액션을 실행합니다.
    private async UniTaskVoid ScheduleDelayUniTask(double startNetworkTime, Action action, CancellationToken token)
    {
        while (PhotonNetwork.Time < startNetworkTime)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
        }

        action?.Invoke();
    }
}
