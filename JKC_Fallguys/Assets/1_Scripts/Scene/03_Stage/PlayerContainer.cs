using System.Collections.Generic;
using Model;
using UniRx;
using UnityEngine;

public class PlayerContainer
{
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    private GameObject _stageDataManager;
    private PlayerObserverCamera _observer;
    private readonly Dictionary<int, GameObject> _playerGameObjects = new Dictionary<int, GameObject>();
    private readonly List<int> _observedIndexList = new List<int>();
    private int _observingIndex = 0;
    
    private ReactiveProperty<GameObject> _targetObject = new ReactiveProperty<GameObject>();

    public void Initialize(GameObject stageDataManager)
    {
        _stageDataManager = stageDataManager;

        InitializeRx();
    }

    private void InitializeRx()
    {
        _targetObject
            .Where(obj => obj != null)
            .Select(obj => obj.ObserveEveryValueChanged(obj => obj.activeSelf))
            .Switch()
            .Subscribe(_ => ObservedNextPlayer())
            .AddTo(_compositeDisposable);
    }
    
    private void SetObservationTarget(GameObject gameObject)
    {
        _targetObject.Value = gameObject;
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

            SetCharacter(character);
            return;
        }
        while (_observingIndex != startIndex);
    }

    private void SetCharacter(Transform character)
    {
        _observer.BindObservedCharacter(character);
        SetObservationTarget(character.parent.gameObject);
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

            SetCharacter(character);
            return;
        }
        while (_observingIndex != startIndex);
    }

    private void SetBindObject()
    {
        
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
        SetObservationTarget(null);
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

    public void OnRelease()
    {
        _compositeDisposable.Dispose();
    }
}
