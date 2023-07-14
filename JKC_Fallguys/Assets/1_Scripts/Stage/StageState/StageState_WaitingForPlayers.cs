using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Util.Helper;

public class StageState_WaitingForPlayers : StageState
{
    private int _readyClientsCount = 0;
    private const int GameStartDelaySeconds = 2;
    private CancellationTokenSource _cts = new CancellationTokenSource();

    protected override void AddToSequenceActionDictionary()
    {
        StageManager.Instance.ObjectRepository.SequenceActionDictionary[ObjectRepository.StageSequence.WaitingForPlayers] = this;
    }

    public override void Action()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            WaitForAllClientsLoaded().Forget();
        }
        
        photonView.RPC("IsClientReady", RpcTarget.MasterClient);
    }
    
    [PunRPC]
    public void IsClientReady()
    {
        ++_readyClientsCount;
    }
    
    private async UniTask WaitForAllClientsLoaded()
    {
        while (_readyClientsCount < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            await UniTask.Delay(TimeSpan.FromMilliseconds(PhotonTimeHelper.SyncIntervalMs));
        }

        ScheduledGameStart(PhotonTimeHelper.GetFutureNetworkTime(GameStartDelaySeconds));
    }

    private void ScheduledGameStart(double startTime)
    {
        photonView.RPC("RpcSetGameStart", RpcTarget.All, startTime);
    }
    
   
    [PunRPC]
    public void RpcSetGameStart(double startPhotonNetworkTime)
    {
        PhotonTimeHelper.ScheduleDelayedAction(startPhotonNetworkTime, SetGameStart, _cts.Token);
    }
    
    private void SetGameStart()
    {
        StageManager.Instance.ObjectRepository.SetSequence(ObjectRepository.StageSequence.PlayersReady);
    }
}
