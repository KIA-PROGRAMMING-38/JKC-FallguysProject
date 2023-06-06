using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    // 방을 생성하거나 기존 방에 가입하는 UI를 관리한다
    [SerializeField] 
    private CreateOrJoinRoomCanvas _createOrJoinRoomCanvas;
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas
    {
        get { return _createOrJoinRoomCanvas; }
    }
    
    // 현재 방의 UI를 관리한다
    [SerializeField] private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }

    private void Awake()
    {
        FirstInitialize();
    }

    /// <summary>
    /// _createOrJoinRoomCanvas와 _currentRoomCanvas를 초기화한다
    /// </summary>
    private void FirstInitialize()
    {
        CreateOrJoinRoomCanvas.FirstInitialize(this);
        CurrentRoomCanvas.FirstInitialize(this);
    }
}