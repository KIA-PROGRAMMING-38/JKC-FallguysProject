using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using LiteralRepository;
using Hashtable = ExitGames.Client.Photon.Hashtable;

/// <summary>
/// 게임 시작을 관리하는 클래스입니다.
/// </summary>
public class PhotonMatchingSceneRoomManager : MonoBehaviourPun
{
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public void SetGameStartStream()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Observable.EveryUpdate()
                .Where(_ => Model.MatchingSceneModel.StartCount == 0)
                .Subscribe(_ => EnterStage())
                .AddTo(_compositeDisposable);
            
            Observable.EveryUpdate()
                .Where(_ => Model.MatchingSceneModel.StartCount == 3)
                .Subscribe(_ => LockUpEntrance())
                .AddTo(_compositeDisposable);
        }
    }
    
    // EnterLevel 메서드는 게임 시작 카운트를 감소시키고, 다음 레벨을 로드합니다.
    private void EnterStage()
    {
        Model.MatchingSceneModel.DecreaseStartCount();
        PhotonNetwork.LoadLevel(SceneIndex.Stage);
    }
    
    // LockUpEntrance 메서드는 현재 방의 입장을 막고, 모든 플레이어에게 개인 인덱스를 부여합니다.
    private void LockUpEntrance()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        Model.MatchingSceneModel.PossibleToEnter(false);

        int index = 1;
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            Hashtable _playerIndex = new Hashtable() { { "PersonalIndex", index }}; 
            player.Value.SetCustomProperties(_playerIndex);
            ++index;
        }
    }
    
    /// <summary>
    /// 플레이어가 방에 입장했을 때 호출됩니다.
    /// </summary>
    [PunRPC]
    public void PlayerEnterTheRoom()
    {
        photonView.RPC("RoomDataUpdate", RpcTarget.MasterClient);
    }
    
    /// <summary>
    /// 룸의 데이터를 업데이트합니다.
    /// </summary>
    [PunRPC]
    public void RoomDataUpdate()
    {
        photonView.RPC("ResetCountDown", RpcTarget.All);
    }

    /// <summary>
    /// 시작 카운트다운을 초기화합니다. 
    /// </summary>
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
