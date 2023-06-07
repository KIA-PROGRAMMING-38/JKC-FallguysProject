using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    // 플레이어 리스트 UI가 생성되어 배치될 트랜스폼
    [SerializeField] private Transform _content;
    // 플레이어 정보를 표시하기 위해 사용되는 프리팹
    [SerializeField] private PlayerListing _playerListing;
    //
    [SerializeField] private Text _readyUpText;

    // 현재 UI에 표시되는 플레이어 리스트를 저장하고 있는 리스트
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomCanvases _roomCanvases;
    private bool _ready;

    public override void OnEnable()
    {
        base.OnEnable();

        SetReadyUp(false);
        // 현재 방에 있는 플레이어를 가져오고 UI를 설정
        GetCurrentRoomPlayers();
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;

        if (_ready)
        {
            _readyUpText.text = "R";
        }
        else
        {
            _readyUpText.text = "N";
        }
    }

    /// <summary>
    /// 내가 방을 떠날 때 호출되는 함수
    /// 방을 떠날 때 OnDisable이 호출된다(SetActive false)
    /// </summary>
    public override void OnDisable()
    {
        for (int i = 0; i < _listings.Count; ++i)
        {
            Destroy(_listings[i].gameObject);
        }
        
        _listings.Clear();
    }

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    /// <summary>
    /// 햔제 방에 있는 모든 플레이어를 가져와 각 플레이어에 대해 AddPlayerListing 호출
    /// </summary>
    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        // == null 이 조건은 현재 클라이언트가 어떤 방에도 참여하고 있지 않은 상황
        // 클라이언트가 아직 방에 들어가지 않았거나 나갔을 경우를 의미
        // Players == null 이 조건은 현재 방에 플레이어가 없는 상황
        // 방이 생성되었지만 아직 플레이어가 들어오지 않았거나, 모든 플레이어가 방을 떠난 경우
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    /// <summary>
    /// 플레이어 정보를 받아 PlayerListing을 인스턴스화하고 세팅하는 함수
    /// </summary>
    /// <param name="player"></param>
    private void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListing, _content);

            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }        
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _roomCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClickLeaveRoom();
    }

    /// <summary>
    /// 플레이어가 방에 들어올 경우 호출되는 콜백
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    /// <summary>
    /// 플레이어가 방에서 나갔을 때 호출되는 콜백
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // 만약 _listing내 otherPlayer와 동일한 객체가 없다면 -1 반환
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public void OnClickStartGame()
    {
        // 현재 동작을 실행하는 클라이언트가 마스터인지
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _listings.Count; ++i)
            {
                if (_listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!_listings[i].Ready)
                        return; 
                }
            }
            
            // 방을 닫는다. 새로운 플레이어가 이 방에 참가하는게 불가능해진다
            PhotonNetwork.CurrentRoom.IsOpen = false;
            // 현재 방을 보이지 않게 설정. 이 설정 후에는 새로운 플레이어가 방을 발견하는 것이 불가능해진다
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void OnClickReadyUp()
    {
        // 버튼을 누르는 클라이언트가 마스터 클라이언트가 아닌 경우
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            // RPC를 마스터 클라이언트에게 보낸다. 이 RPC는 마스터 클라이언트가 준비 상태를 업데이트 하도록 만든다 
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer, _ready);
        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        // 만약 _listing내 otherPlayer와 동일한 객체가 없다면 -1 반환
        // 해당 플레이어를 찾아 그 플레이어의 준비 상태를 업데이트한다
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].Ready = ready;
        }
    }
}
