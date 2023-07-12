using System;
using System.Threading;
using Model;
using Photon.Pun;
using UniRx;

public class StageState_PlayersReady : StageState
{
    private const int OperationCountdownDelaySeconds = 3;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    
    public override void AddToDictionary()
    {
        StageManager.Instance.StageDataManager.SequenceActionDictionary[StageDataManager.StageSequence.PlayersReady] = this;
    }

    public override void Action()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        ScheduleOperationCountdown(StageManager.Instance.PhotonTimeHelper.GetFutureNetworkTime(OperationCountdownDelaySeconds));
    }
    
    private void ScheduleOperationCountdown(double startTime)
    {
        photonView.RPC("RpcStartTriggerOperationCountDown", RpcTarget.All, startTime);
    }

    [PunRPC]
    public void RpcStartTriggerOperationCountDown(double startPhotonNetworkTime)
    {
        StageManager.Instance.PhotonTimeHelper.ScheduleDelayedAction(startPhotonNetworkTime, StartOperationCountdown, _cts.Token);
    }

    private void StartOperationCountdown()
    {
        Observable.Interval(TimeSpan.FromSeconds(1.5))
            .TakeWhile(_ => StageSceneModel.SpriteIndex.Value <= 4)
            .Subscribe(_ => StageSceneModel.IncreaseCountDownIndex())
            .AddTo(this);
    }
}
