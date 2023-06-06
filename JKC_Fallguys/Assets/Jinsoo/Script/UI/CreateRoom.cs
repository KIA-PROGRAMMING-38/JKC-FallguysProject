using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] 
    private TMP_InputField _roomName;

    private RoomCanvases _roomCanvases;
    
    /// <summary>
    /// 초기화 작업 수행
    /// RoomCanvases 객체를 전달받아 참조 연결
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }
    
    /// <summary>
    /// 사용자가 방을 생성하기 위해 클릭하는 버튼
    /// </summary>
    public void OnCLickCreateRoom()
    {
        // 네트워크 연결 상태 확인
        if  (!PhotonNetwork.IsConnected)
        {
            return;
        }
        
        // 방을 생성하기 전, 생성할 방에 대한 옵션 설정
        RoomOptions options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        options.PublishUserId = true;
        options.MaxPlayers = 8;
        // 방을 생성하거나 조인
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    /// <summary>
    /// 방이 성공적으로 생성된 후에 호출
    /// </summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.", this);

        _roomCanvases.CurrentRoomCanvas.Show();
    }

    /// <summary>
    /// 방이 생성됨에 실패했을 때 호출 됨
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message, this);
    }
}
