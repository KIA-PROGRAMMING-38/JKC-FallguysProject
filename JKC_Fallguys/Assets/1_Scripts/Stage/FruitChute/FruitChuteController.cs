using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Model;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class FruitChuteController : StageController
{
    private Camera _observeCamera;
    private FruitPooler _fruitPooler;
    private CanonController _canonController;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    protected override void Awake()
    {
        base.Awake();
        
        _fruitPooler = transform.Find("FruitPooler").GetComponent<FruitPooler>();
        Debug.Assert(_fruitPooler != null);
        _canonController = transform.Find("CanonController").GetComponent<CanonController>();
        Debug.Assert(_canonController != null);

        _canonController.Initialize(_fruitPooler);
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
        StageManager.Instance.ObjectRepository.SetSequence(ObjectRepository.StageSequence.GameCompletion);

        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if (StageManager.Instance.PlayerRepository.GetCurrentState(actorNumber).Value != PlayerRepository.PlayerState.Victory)
        {
            StageManager.Instance.PlayerRepository.SetPlayerState(actorNumber, PlayerRepository.PlayerState.Defeat);
            photonView.RPC
                ("RpcAddFailedPlayer", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
        }
        else if (StageManager.Instance.PlayerRepository.GetCurrentState(actorNumber).Value == PlayerRepository.PlayerState.Victory)
        {
            StageManager.Instance.PlayerRepository.SetPlayerState(actorNumber, PlayerRepository.PlayerState.GameTerminated);
        }
    }

    [PunRPC]
    public void RpcAddFailedPlayer(int actorNumber)
    {
        StageManager.Instance.PlayerRepository.AddFailedPlayer(actorNumber);
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}