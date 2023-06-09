using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonMatchingSceneEventManager : MonoBehaviourPunCallbacks
{
    private PhotonMatchingSceneRoomManager _roomManager;

    public void OnInitialize(PhotonMatchingSceneRoomManager roomManager)
    {
        _roomManager = roomManager;
    }
 
    /// <summary>
    /// Photon 서버와의 연결이 끊겼을 때 호출되는 콜백 메서드
    /// </summary>
    /// <param name="cause">연결이 끊긴 이유.</param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
    }
    
    public override void OnJoinedRoom()
    {
        _roomManager.PlayerEnterTheRoom();
        _roomManager.SetGameStartStream();
        
        PhotonNetwork.AutomaticallySyncScene = true;

        Debug.Log("Joined to a room successfully");
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 사용 가능한 방이 없을 때 새로운 방 생성
        // null을 전달하면 무작위 방 생성
        PhotonNetwork.CreateRoom(null);
    }
    
    /// <summary>
    /// 로비에 성공적으로 접속하였을 때 호출되는 콜백 메서드
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
}
