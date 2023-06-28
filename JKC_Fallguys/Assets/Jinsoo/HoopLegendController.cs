using Photon.Pun;
using UniRx;

public class HoopLegendController : StageController
{
    protected override void SetGameTime()
    {
        remainingGameTime.Value = 60;
    }

    protected override void InitializeRx()
    {
        StageDataManager.Instance.IsGameActive
            .Where(state => state)
            .Subscribe(_ => --remainingGameTime.Value)
            .AddTo(this);

        remainingGameTime
            .Subscribe(_ => GameStartBroadCast())
            .AddTo(this);

        remainingGameTime
            .Where(count => remainingGameTime.Value == 0)
            .Subscribe(_ => RpcEndGame())
            .AddTo(this);
    }
    
    private void GameStartBroadCast()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RpcCountDown", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RpcCountDown()
    {
        --remainingGameTime.Value;
    }

    private void RpcEndGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RpcEndGame", RpcTarget.All);
        }
    }

    [PunRPC]
    public void EndGame()
    {
        StageDataManager.Instance.IsGameActive.Value = false;
        StageDataManager.Instance.CurrentState.Value = StageDataManager.PlayerState.GameTerminated;
    }
}
