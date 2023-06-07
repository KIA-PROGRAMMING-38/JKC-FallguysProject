using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomCanvases _roomCanvases;
    
    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }
    
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _roomCanvases.CurrentRoomCanvas.Hide();
    }
}
