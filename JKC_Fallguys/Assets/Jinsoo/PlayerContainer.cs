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
        int startIndex = _observingIndex;
        while (true)
        {
            _observingIndex = (_observingIndex + 1) % _observingList.Count;

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

            if (_observingIndex == startIndex)
            {
                Debug.Log("No valid GameObject found");
                return;
            }
        }
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

            if (_observingIndex == startIndex)
            {
                Debug.Log("No valid GameObject found");
                return;
            }
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
