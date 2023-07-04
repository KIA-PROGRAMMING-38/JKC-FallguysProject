using LiteralRepository;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestNetworkConnector : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Connecting to Server...");
        
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        
        StageDataManager.Instance.IsGameActive.Value = true;
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server...");

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
    
    /// <summary>
    /// Photon 서버와의 연결이 끊겼을 때 호출되는 콜백 메서드입니다.
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
    }
    
    /// <summary>
    /// 로비에 성공적으로 접속하였을 때 호출되는 콜백 메서드입니다.
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Create Room");
        
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }
}
