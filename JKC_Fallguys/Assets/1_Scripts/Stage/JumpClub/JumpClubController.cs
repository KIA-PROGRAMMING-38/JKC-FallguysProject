using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Model;
using Photon.Pun;
using UniRx;
using UnityEngine;

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
        StageSceneModel.SetRemainingTime(60);
    }

    protected override void InitializeRx()
    {
        StageManager.Instance.StageDataManager.CurrentSequence
            .Where(sequence => sequence == StageDataManager.StageSequence.GameInProgress)
            .Subscribe(_ => GameStartBroadCast())
            .AddTo(this);

        StageSceneModel.RemainingTime
            .Where(RemainingTime => RemainingTime == 0)
            .Subscribe(_ => EndGame())
            .AddTo(this);
    }

    private async UniTaskVoid CountDown(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancelToken);
            
            StageSceneModel.DecreaseRemainingTime();

            if (StageSceneModel.RemainingTime.Value == 0)
                break;
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
        StageManager.Instance.StageDataManager.SetSequence(StageDataManager.StageSequence.GameCompletion);
        
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if (StageManager.Instance.PlayerContainer.IsPlayerActive(actorNumber).Value)
        {
            StageManager.Instance.PlayerContainer.SetPlayerState(actorNumber, PlayerContainer.PlayerState.Victory);
            
            photonView.RPC("RpcDeclarationOfVictory", RpcTarget.All, actorNumber);
        }
        else
        {
            StageManager.Instance.PlayerContainer.SetPlayerState(actorNumber, PlayerContainer.PlayerState.GameTerminated);
        }
    }

    [PunRPC]
    public void RpcDeclarationOfVictory(int actorNumber)
    {
        StageManager.Instance.PlayerContainer.StagePlayerRankings.Add(actorNumber);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
