using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonMatchingSceneRoomManager : MonoBehaviourPun
{
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public void SetGameStartStream()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Observable.EveryUpdate()
                .Where(_ => Model.MatchingSceneModel.StartCount == 0)
                .Subscribe(_ => EnterLevel())
                .AddTo(_compositeDisposable);
            
            Observable.EveryUpdate()
                .Where(_ => Model.MatchingSceneModel.StartCount == 3)
                .Subscribe(_ => LockUpEntrance())
                .AddTo(_compositeDisposable);
        }
    }
    
    private void EnterLevel()
    {
        Model.MatchingSceneModel.DecreaseStartCount();
        PhotonNetwork.LoadLevel(1);
    }
    
    private void LockUpEntrance()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;

        int index = 1;
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            Hashtable _playerIndex = new Hashtable() { { "PersonalIndex", index }}; 
            player.Value.SetCustomProperties(_playerIndex);
            ++index;
        }
    }
    
    [PunRPC]
    public void PlayerEnterTheRoom()
    {
        photonView.RPC("RoomDataUpdate", RpcTarget.MasterClient);
    }
    
    [PunRPC]
    public void RoomDataUpdate()
    {
        photonView.RPC("ResetCountDown", RpcTarget.All);
    }

    [PunRPC]
    public void ResetCountDown()
    {
        Model.MatchingSceneModel.ResetStartCount();
    }

    private void OnDestroy()
    {
        _compositeDisposable.Dispose();
    }
}
