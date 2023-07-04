using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UniRx;

public class JumpClubController : StageController
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
            .Subscribe(_ => EndGame())
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
        StageDataManager.Instance.SetGameStatus(false);
        
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if (StageDataManager.Instance.IsPlayerActive(actorNumber).Value)
        {
            StageDataManager.Instance.SetPlayerState(actorNumber, StageDataManager.PlayerState.Victory);
            
            photonView.RPC("RpcDeclarationOfVictory", RpcTarget.All, actorNumber);
        }
        else
        {
            StageDataManager.Instance.SetPlayerState(actorNumber, StageDataManager.PlayerState.Defeat);
        }
    }

    [PunRPC]
    public void RpcDeclarationOfVictory(int actorNumber)
    {
        StageDataManager.Instance.StagePlayerRankings.Add(actorNumber);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
