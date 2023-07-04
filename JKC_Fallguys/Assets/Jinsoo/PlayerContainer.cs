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
        ++_observingIndex;

        if (_observingIndex == _observingList.Count)
        {
            _observingIndex = 0;
        }

        _observer.BindObservedCharacter
            (_observingList[_observingIndex].transform.Find("Character").GetComponent<Transform>());
    }

    public void ObservedPrevPlayer()
    {
        --_observingIndex;

        if (_observingIndex < 0)
        {
            _observingIndex = _observingList.Count - 1;
        }
        
        _observer.BindObservedCharacter
            (_observingList[_observingIndex].transform.Find("Character").GetComponent<Transform>());
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

        FindAllObservedObjects(_stageDataManager);
    }
    
    private void FindAllObservedObjects(GameObject parent)
    {
        Transform parentTransform = parent.transform;
    
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
        
            _observingList.Add(childObject);
        }
    }
}
