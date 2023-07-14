using System;
using System.Threading;
using Model;
using Photon.Pun;
using UniRx;
using Util.Helper;

public class StageState_PlayersReady : StageState
{
    private const int OperationCountdownDelaySeconds = 3;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    protected override void AddToSequenceActionDictionary()
    {
        StageManager.Instance.ObjectRepository.SequenceActionDictionary[ObjectRepository.StageSequence.PlayersReady] = this;
    }

    public override void Action()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        ScheduleOperationCountdown(PhotonTimeHelper.GetFutureNetworkTime(OperationCountdownDelaySeconds));
    }
    
    private void ScheduleOperationCountdown(double startTime)
    {
        photonView.RPC("RpcStartTriggerOperationCountDown", RpcTarget.All, startTime);
    }

    [PunRPC]
    public void RpcStartTriggerOperationCountDown(double startPhotonNetworkTime)
    {
        PhotonTimeHelper.ScheduleDelayedAction(startPhotonNetworkTime, StartOperationCountdown, _cts.Token);
    }

    private void StartOperationCountdown()
    {
        Observable.Interval(TimeSpan.FromSeconds(1.5))
            .TakeWhile(_ => StageSceneModel.SpriteIndex.Value <= 4)
            .Subscribe(_ => StageSceneModel.IncreaseCountDownIndex())
            .AddTo(this);
    }
}
