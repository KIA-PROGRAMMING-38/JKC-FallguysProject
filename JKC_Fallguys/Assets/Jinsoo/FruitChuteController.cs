using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class FruitChuteController : StageController
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
            .ObserveEveryValueChanged(_ => remainingGameTime)
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

        if (StageDataManager.Instance.CurrentState.Value != StageDataManager.PlayerState.Victory)
        {
            StageDataManager.Instance.CurrentState.Value = StageDataManager.PlayerState.Defeat;
        }
    }
}
