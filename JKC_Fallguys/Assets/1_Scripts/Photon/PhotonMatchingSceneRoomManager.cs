using System.Collections.Generic;
using System.IO;
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

    private void EnterGameLoading()
    {
        Model.MatchingSceneModel.DecreaseStartCount();
        SceneChangeHelper.ChangeNetworkLevel(SceneIndex.GameLoading);
    }

    // LockUpEntrance 메서드는 현재 방의 입장을 막고, 각 플레이어에게 개인 ActorNumber를 부여합니다.
    private void LockUpEntrance()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        Model.MatchingSceneModel.PossibleToExit(false);
        Model.StageSceneModel.SetObservedPlayerActorName(PhotonNetwork.MasterClient.NickName);
    
        photonView.RPC("RpcSetMapData", RpcTarget.All);

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            int actorNumber = player.Value.ActorNumber;
            int playerTextureIndex = (int)player.Value.CustomProperties["PlayerTextureIndex"];
            StageManager.Instance.PlayerContainer.PlayerDataByIndex[actorNumber] =
                new PlayerData(player.Value.NickName, playerTextureIndex, 0);
        }
        
        StageManager.Instance.Initialize();
    }

    [PunRPC]
    public void RpcSetMapData()
    {
        MapInstanceLoad().Forget();
    }
    
    #pragma warning disable CS1998
    private async UniTaskVoid MapInstanceLoad()
    #pragma warning restore CS1998
    {
        for (int i = 0; i < StageDataManager.MaxPlayableMaps; ++i)
        {
            MapData mapData = ResourceManager.JsonLoader<MapData>($"Data/MapData_{i:D2}");
            StageManager.Instance.StageDataManager.MapDatas.Add(i, mapData);
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
