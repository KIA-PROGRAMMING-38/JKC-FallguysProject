using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    // 방 리스트 UI가 생성되어 배치될 위치를 나타내는 Transform
    [SerializeField] 
    private Transform _content;
    // 방 정보를 표시하기 위해 사용되는 프리팹
    [SerializeField] 
    private RoomListing _roomListing;

    // 현재 UI에 표시되고 있는 방 리스트를 저장하고 있는 List
    private List<RoomListing> _listings = new List<RoomListing>();
    // 화면에 표시되는 여러 캔버스를 관리하는 객체
    private RoomCanvases _roomCanvases;

    /// <summary>
    /// 참조 연결
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    /// <summary>
    /// 플레이어가 방에 성공적으로 입장할 시 호출되는 콜백
    /// </summary>
    public override void OnJoinedRoom()
    {
        _roomCanvases.CurrentRoomCanvas.Show();
        _content.DestroyChildren();
        _listings.Clear();
    }

    /// <summary>
    /// 방 리스트 업데이트가 발생하면 호출되는 콜백
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // 방이 제거될 경우
            if (info.RemovedFromList)
            {
                // 해당 방을 _listings에서 찾아 UI를 파괴하고 리스트에서 제거
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            // 방이 추가된 경우
            else
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    // 새로운 RoomListing 인스턴스 생성
                    RoomListing listing = Instantiate(_roomListing, _content);

                    if (listing != null)
                    {
                        // 룸 초기설정, 리스트 추가
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }    
                }
                else
                {
                    
                }
            }
        }
    }
}
