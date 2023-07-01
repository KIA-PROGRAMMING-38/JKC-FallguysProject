using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UniRx;

public class HoopLegendController : StageController
{
    private CancellationTokenSource _cancellationTokenSource;

    protected override void Awake()
    {
        base.Awake();
        
        _cancellationTokenSource = new CancellationTokenSource();
    }

    protected override void SetGameTime()
    {
        remainingGameTime.Value = 60;
    }

    protected override void InitializeRx()
    {
        StageDataManager.Instance.IsGameActive
            .Where(state => state)
            .Subscribe(_ => GameStartBroadCast())
            .AddTo(this);

        remainingGameTime
            .Where(count => remainingGameTime.Value == 0)
            .Subscribe(_ => RpcEndGame())
            .AddTo(this);
    }
    
    private async UniTaskVoid CountDown(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancelToken);
            
            --remainingGameTime.Value;
        }
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
        CountDown(_cancellationTokenSource.Token).Forget();
    }

    private void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RpcEndGame", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RpcEndGame()
    {
        StageDataManager.Instance.IsGameActive.Value = false;
        
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageDataManager.Instance.SetPlayerState(actorNumber, StageDataManager.PlayerState.GameTerminated);
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
