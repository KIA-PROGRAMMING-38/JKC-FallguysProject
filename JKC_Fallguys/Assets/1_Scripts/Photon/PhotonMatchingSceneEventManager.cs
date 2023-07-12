using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Model;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonMatchingSceneEventManager : MonoBehaviourPunCallbacks
{
    private PhotonMatchingSceneRoomManager _roomManager;
    private CancellationTokenSource _cts = new CancellationTokenSource();

    private void Awake()
    {
        _joinLobbyFlag = false;
        
        Initialize().Forget();
    }
 
    /// <summary>
    /// Photon 서버와의 연결이 끊겼을 때 호출되는 콜백 메서드
    /// </summary>
    /// <param name="cause">연결이 끊긴 이유.</param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
    }

    private bool _joinRandomFailed = false;
    private bool _joinLobbyFlag = false;
    private async UniTaskVoid Initialize()
    {
        float elapsedTime = 0;
        while (elapsedTime < 3f)
        {
            PhotonNetwork.JoinRandomRoom();
            elapsedTime += 0.3f;

            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: _cts.Token);
        }

        if (_joinRandomFailed) 
        {
            // if(PhotonNetwork.CountOfRooms == 0) 
            // {
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 12;

                PhotonNetwork.CreateRoom(null, roomOptions);
            // }
            // else
            // {
                // _joinLobbyFlag = true;
                // PhotonNetwork.JoinRandomRoom();
            // }

            _joinRandomFailed = false;
        }
    }
    
    public override void OnJoinedRoom()
    {
        OnInstantiatePhotonRoomManager();
        OnInitializeJoinRoom();
        SetMyTextureProperty();

        Debug.Log("Joined to a room successfully");
    }

    private void SetMyTextureProperty()
    {
        Player localPlayer = PhotonNetwork.LocalPlayer;
        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

        playerProperties["PlayerTextureIndex"] = LobbySceneModel.PlayerTextureIndex.Value;

        localPlayer.SetCustomProperties(playerProperties);
    }

    private void OnInitializeJoinRoom()
    {
        MatchingSceneModel.RoomAdmissionStatus(true);
        
        _roomManager.PlayerEnterTheRoom();
        _roomManager.SetGameStartStream();
        
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void OnInstantiatePhotonRoomManager()
    {
        string filePath = Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.Matching, "PhotonMatchingSceneRoomManager");
        
        PhotonMatchingSceneRoomManager roomManager = 
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation)
            .GetComponent<PhotonMatchingSceneRoomManager>();

        _roomManager = roomManager;
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _joinRandomFailed = true;
        
        if (_joinLobbyFlag)
        {
            SceneChangeHelper.ChangeLocalScene(SceneIndex.Lobby);
        }
    }
    
    /// <summary>
    /// 로비에 성공적으로 접속하였을 때 호출되는 콜백 메서드
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public override void OnLeftRoom()
    {
        if (MatchingSceneModel.IsEnterPhotonRoom.Value == false)
        {
            SceneChangeHelper.ChangeLocalScene(SceneIndex.Lobby);
        }
    }
}
