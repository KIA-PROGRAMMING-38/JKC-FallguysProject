using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Model;
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
        StageSceneModel.SetRemainingTime(60);
    }

    protected override void InitializeRx()
    {
        StageManager.Instance.ObjectRepository.CurrentSequence
            .Where(sequence => sequence == ObjectRepository.StageSequence.GameInProgress)
            .Subscribe(_ => GameStartBroadCast())
            .AddTo(this);

        StageSceneModel.RemainingTime
            .Where(RemainingTime => RemainingTime == 0)
            .Subscribe(_ => EndGame())
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
        CountDown(_cancellationTokenSource.Token).Forget();
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
        StageManager.Instance.ObjectRepository.SetSequence(ObjectRepository.StageSequence.GameCompletion);
        
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageManager.Instance.PlayerContainer.SetPlayerState(actorNumber, PlayerContainer.PlayerState.GameTerminated);
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
