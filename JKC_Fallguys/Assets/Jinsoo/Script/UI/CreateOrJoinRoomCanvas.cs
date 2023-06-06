using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField] 
    private CreateRoom _createRoomMenu;

    [SerializeField] 
    private RoomListingMenu _roomListingMenu;
    
    private RoomCanvases _roomCanvases;
    
    /// <summary>
    /// 참조를 연결한다
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
        _createRoomMenu.FirstInitialize(canvases);
        _roomListingMenu.FirstInitialize(canvases);
    }
}
