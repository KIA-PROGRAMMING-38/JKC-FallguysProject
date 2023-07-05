using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer
{
    private GameObject _stageDataManager;
    private PlayerObserverCamera _observer;
    private readonly List<GameObject> _observingList = new List<GameObject>();
    private int _observingIndex = 0;

    public void Initialize(GameObject stageDataManager)
    {
        _stageDataManager = stageDataManager;
    }
    
    public void ObservedNextPlayer()
    {
        Debug.Log($"InitNextPlayer: {_observingList.Count}");
        
        if (_observingList.Count == 0)
        {
            return;
        }

        int startIndex = _observingIndex;
        do
        {
            Debug.Log($"PrevObservingIndex: {_observingIndex}");
            _observingIndex = (_observingIndex + 1) % _observingList.Count;
            Debug.Log($"NextObservingIndex: {_observingIndex}");
            
            GameObject current = _observingList[_observingIndex];
            Debug.Log($"First: {current.name}");
            if (current == null || !current.activeSelf)
                continue;

            Debug.Log($"Second: {_observingList[_observingIndex].name}");
            Transform character = current.transform.Find("Character");
            if (character == null)
                continue;

            _observer.BindObservedCharacter(character);
            Debug.Log($"DoWhile: {character}");
            return;
        }
        while (_observingIndex != startIndex);
    }


    public void ObservedPrevPlayer()
    {
        int startIndex = _observingIndex;
        while (true)
        {
            _observingIndex = (_observingIndex - 1 + _observingList.Count) % _observingList.Count;

            GameObject current = _observingList[_observingIndex];
            if (current == null)
                continue;

            if (!current.activeSelf)
                continue;

            Transform character = current.transform.Find("Character");
            if (character == null)
                continue;

            _observer.BindObservedCharacter(character);
            return;
        }
    }

    public void BindObservingCamera(PlayerObserverCamera observer)
    {
        _observer = observer;
    }
    
    public void Clear()
    {
        _observingList.Clear();
        _observingIndex = 0;
        _observer = default;
    }
    
    public void FindAllObservedObjects()
    {
        Transform parentTransform = _stageDataManager.transform;
    
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
        
            _observingList.Add(childObject);
            Debug.Log(childObject.name);
        }
    }
}
