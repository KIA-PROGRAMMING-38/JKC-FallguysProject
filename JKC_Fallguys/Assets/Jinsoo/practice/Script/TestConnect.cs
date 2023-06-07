using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestConnect : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Connecting to Server...");

        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 5;
        
        // 마스터 클라이언트가 씬을 로드하거나 변경할 때 다른 모든 플레이어가 같은 씬을 로드하도록 하는 설정
        PhotonNetwork.AutomaticallySyncScene = true;
        // 현재 사용자(플레이어)의 닉네임을 설정하거나 가져오는 데 사용
        // 이 프로퍼티를 통해 사용자는 게임 서버와 다른 플레이어들에게 자신을 식별할 수 있는 이름 제공
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        // 게임의 버전을 설정하거나 가져오는데 사용
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        // 서버에 연결 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Photon 서버에 성공적으로 연결되었을 때 호출되는 콜백 메서드
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server...", this);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName, this);
        
        // 로비로의 접속 시도
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// Photon 서버와의 연결이 끊겼을 때 호출되는 콜백 메서드
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString(), this);
    }

    /// <summary>
    /// 로비에 성공적으로 접속하였을 때 호출되는 콜백 메서드
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
}
