using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomListing : MonoBehaviour
{
    [SerializeField] 
    private Text _text;

    // 현재 방에 대한 정보를 저장하는 Photon.Realtime의 속성
    public RoomInfo RoomInfo { get; private set; }
    
    /// <summary>
    /// RoomInfo 객체를 받아 방을 세팅한다
    /// </summary>
    /// <param name="roomInfo"></param>
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }

    /// <summary>
    /// 사용자가 방의 정보를 나타내는 버튼을 클릭했을 때 호출된다
    /// </summary>
    public void OnClickButton()
    {
        // RoomInfo.Name은 참가하려는 방의 이름
        // JoinRoom에서 그를 인수로 받을 때는 특정 이름의 방에 참가하도록 요청한다
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
