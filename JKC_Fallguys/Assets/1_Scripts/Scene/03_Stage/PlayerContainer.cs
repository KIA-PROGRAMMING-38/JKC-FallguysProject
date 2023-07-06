using System.Collections.Generic;
using Model;
using UnityEngine;

public class PlayerContainer
{
    private GameObject _stageDataManager;
    private PlayerObserverCamera _observer;
    private readonly Dictionary<int, GameObject> _playerGameObjects = new Dictionary<int, GameObject>();
    private readonly List<int> _observedIndexList = new List<int>();
    private int _observingIndex = 0;

    public void Initialize(GameObject stageDataManager)
    {
        _stageDataManager = stageDataManager;
    }
    
    public void ObservedNextPlayer()
    {
        if (_playerGameObjects.Count == 0)
        {
            return;
        }

        int startIndex = _observingIndex;
        do
        {
            _observingIndex = (_observingIndex + 1) % _observedIndexList.Count;
            
            GameObject current = _playerGameObjects[_observedIndexList[_observingIndex]];
            if (current == null || !current.activeSelf)
                continue;

            Transform character = current.transform.Find("Character");
            if (character == null)
                continue;

            _observer.BindObservedCharacter(character);
            PlayerReferenceManager refManager = current.GetComponent<PlayerReferenceManager>();
            StageSceneModel.SetObservedPlayerActorName(refManager.ArchievePlayerNickName); 
            return;
        }
        while (_observingIndex != startIndex);
    }


    public void ObservedPrevPlayer()
    {
        if (_playerGameObjects.Count == 0)
        {
            return;
        }

        int startIndex = _observingIndex;
        do
        {
            _observingIndex = (_observingIndex - 1 + _observedIndexList.Count) % _observedIndexList.Count;

            GameObject current = _playerGameObjects[_observedIndexList[_observingIndex]];
            if (current == null || !current.activeSelf)
                continue;

            Transform character = current.transform.Find("Character");
            if (character == null)
                continue;

            _observer.BindObservedCharacter(character);
            PlayerReferenceManager refManager = current.GetComponent<PlayerReferenceManager>();
            StageSceneModel.SetObservedPlayerActorName(refManager.ArchievePlayerNickName); 
            return;
        }
        while (_observingIndex != startIndex);
    }


    public void BindObservingCamera(PlayerObserverCamera observer)
    {
        _observer = observer;
    }
    
    public void Clear()
    {
        _playerGameObjects.Clear();
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
            PlayerReferenceManager playerReferenceManager = childObject.GetComponent<PlayerReferenceManager>();

            _playerGameObjects[playerReferenceManager.ArchievePlayerActorNumber] = childObject;
            _observedIndexList.Add(playerReferenceManager.ArchievePlayerActorNumber);
        }
    }
}
