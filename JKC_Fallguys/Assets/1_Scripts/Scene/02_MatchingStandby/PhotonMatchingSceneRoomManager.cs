using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using LiteralRepository;

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
            Model.MatchingSceneModel.StartCount
                .Where(count => count == 0)
                .Subscribe(_ => EnterGameLoading())
                .AddTo(_compositeDisposable);

            Model.MatchingSceneModel.StartCount
                .Where(count => count == 3)
                .Subscribe(_ => LockUpEntrance())
                .AddTo(_compositeDisposable);
        }
    }

    // EnterLevel 메서드는 게임 시작 카운트를 감소시키고, 다음 레벨을 로드합니다.
    private void EnterGameLoading()
    {
        Model.MatchingSceneModel.DecreaseStartCount();
        PhotonNetwork.LoadLevel(SceneIndex.GameLoading);
    }

    // LockUpEntrance 메서드는 현재 방의 입장을 막고, 각 플레이어에게 개인 ActorNumber를 부여합니다.
    private void LockUpEntrance()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        Model.MatchingSceneModel.PossibleToExit(false);
        
        photonView.RPC("RpcSetMapdatas", RpcTarget.All);

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            int actorNumber = player.Value.ActorNumber;
            // 새로운 PlayerData 객체를 만들고, 이를 PlayerScoresByIndex 딕셔너리에 추가합니다.
            StageDataManager.Instance.PlayerDataByIndex[actorNumber] =
                new PlayerData(PhotonNetwork.LocalPlayer.NickName, 0, 0);
        }
    }

    [PunRPC]
    public void RpcSetMapdatas()
    {
        MapInstanceLoad().Forget();
    }
    
    #pragma warning disable CS1998
    private async UniTaskVoid MapInstanceLoad()
    {
        for (int i = 0; i < DataManager.MaxPlayableMaps; ++i)
        {
            MapData mapData = DataManager.JsonLoader<MapData>($"JSON/MapData_{i:D2}");
            StageDataManager.Instance.MapDatas.Add(i, mapData);
        }
    }
    #pragma warning restore CS1998

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
