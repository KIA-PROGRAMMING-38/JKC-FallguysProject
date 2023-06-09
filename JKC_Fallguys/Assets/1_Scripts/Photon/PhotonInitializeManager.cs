using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonInitializeManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Connecting to Server...");

        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else if (!PhotonNetwork.InLobby)
        {
            JoinLobby();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server...");

        if (!PhotonNetwork.InLobby)
        {
            JoinLobby();
        }
    }

    private void JoinLobby()
    {
        PhotonNetwork.JoinLobby();

        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 20;
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
    }
}
