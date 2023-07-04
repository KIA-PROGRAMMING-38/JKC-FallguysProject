using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer
{
    private Transform _observerTransform;
    private readonly Dictionary<int, GameObject> _playerContainter = new Dictionary<int, GameObject>();
    private readonly List<int> _observingList = new List<int>();
    private int _observingIndex = 0;

    
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
}
