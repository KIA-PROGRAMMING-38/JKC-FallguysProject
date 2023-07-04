using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerContainer
{
    private Transform _observerTransform;
    private readonly Dictionary<int, GameObject> _playerContainter = new Dictionary<int, GameObject>();
    private readonly List<int> _observingList = new List<int>();
    private int _observingIndex = 0;

    // 현재 클라이언트를 플레이하고 있는 플레이어의 생존 여부를 나타냅니다.
    private readonly Dictionary<int, ReactiveProperty<bool>> _isPlayerActiveDict = new Dictionary<int, ReactiveProperty<bool>>();
    public IReactiveProperty<bool> IsPlayerActive(int actorNumber)
    {
        if (!_isPlayerActiveDict.ContainsKey(actorNumber))
        {
            _isPlayerActiveDict[actorNumber] = new ReactiveProperty<bool>(true);
        }

        return _isPlayerActiveDict[actorNumber];
    }
        
    // 클라이언트 별 PlayerState를 관리하는 Dictionary입니다.
    private readonly Dictionary<int, ReactiveProperty<PlayerState>> _clientStates = new Dictionary<int, ReactiveProperty<PlayerState>>();

    public IReactiveProperty<PlayerState> GetCurrentState(int actorNumber)
    {
        if (!_clientStates.ContainsKey(actorNumber))
        {
            _clientStates[actorNumber] = new ReactiveProperty<PlayerState>();
        }

        return _clientStates[actorNumber];
    }
    
    // Round Result Panel의 성공, 실패, 종료 여부를 설정하기 위한 변수입니다.
    public enum PlayerState
    {
        Default,
        Victory,
        Defeat,
        GameTerminated
    }

    public void ObservedNextPlayer()
    {
        ++_observingIndex;

        if (_observingIndex == _playerContainter.Count)
        {
            _observingIndex = 0;
        }
    }

    public void ObservedPrevPlayer()
    {
        --_observingIndex;

        if (_observingIndex < 0)
        {
            _observingIndex = _playerContainter.Count - 1;
        }
        
        _observerTransform.SetParent(_playerContainter[_observingIndex].transform);
    }

    public void AddPlayer(int actorNumber, GameObject player)
    {
        _playerContainter.Add(actorNumber, player);
    }

    public void BindObservingCamera(Transform observerTransform)
    {
        _observerTransform = observerTransform;
    }
    
    public void Clear()
    {
        _playerContainter.Clear();
        _observingList.Clear();
        _observingIndex = 0;
        _observerTransform = default;
        
        foreach (int key in _playerContainter.Keys)
        {
            _observingList.Add(key);
        }
    }
    
    public void SetPlayerState(int actorNumber, PlayerState state)
    {
        GetCurrentState(actorNumber).Value = state;
    }

    public void SetPlayerAlive(int actorNumber, bool status)
    {
        IsPlayerActive(actorNumber).Value = status;
    }
}
