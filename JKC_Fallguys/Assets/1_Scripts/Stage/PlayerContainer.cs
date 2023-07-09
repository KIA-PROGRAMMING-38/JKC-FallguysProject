using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerContainer
{
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    private GameObject _stageDataManager;
    private PlayerObserverCamera _observer;
    private readonly Dictionary<int, GameObject> _playersByActorNumber = new Dictionary<int, GameObject>();
    private readonly List<int> _actorNumbersOfPlayers = new List<int>();
    private int _currentPlayerIndexInActorNumbers = 0;
    private bool _isObservationInProgress = false;

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
            .DistinctUntilChanged()
            .Subscribe(_ => ObservedNextPlayer())
            .AddTo(_compositeDisposable);
    }
    
    private void ChangeObservingPlayer(GameObject gameObject)
    {
        _targetObject.Value = gameObject;
    }


    private void SetCharacter(Transform character)
    {
        _observer.BindObservedCharacter(character);
        ChangeObservingPlayer(character.parent.gameObject);
    }

    public void ObservedNextPlayer()
    {
        int startIndex = (_currentPlayerIndexInActorNumbers + 1) % _actorNumbersOfPlayers.Count;
        SetObservingPlayer(startIndex);
    }

    public void ObservedPrevPlayer()
    {

        int startIndex = (_currentPlayerIndexInActorNumbers - 1 + _actorNumbersOfPlayers.Count) % _actorNumbersOfPlayers.Count;
        SetObservingPlayer(startIndex);
    }

    private void SetObservingPlayer(int startIndex)
    {
        if (_playersByActorNumber.Count == 0 || _isObservationInProgress)
        {
            return;
        }
        
        _isObservationInProgress = true;
        _currentPlayerIndexInActorNumbers = startIndex;

        do
        {
            GameObject current = _playersByActorNumber[_actorNumbersOfPlayers[_currentPlayerIndexInActorNumbers]];
            if (current == null || !current.activeSelf)
                continue;

            Transform character = current.transform.Find("Character");
            if (character == null)
                continue;

            SetCharacter(character);
            _isObservationInProgress = false;
            return;
        }
        while ((_currentPlayerIndexInActorNumbers = (_currentPlayerIndexInActorNumbers + 1) % _actorNumbersOfPlayers.Count) != startIndex);

        _observer.gameObject.SetActive(false);
        _isObservationInProgress = false;
    }



    public void BindObservingCamera(PlayerObserverCamera observer)
    {
        _observer = observer;
    }
    
    public void Clear()
    {
        _playersByActorNumber.Clear();
        _currentPlayerIndexInActorNumbers = 0;
        _observer = default;
        ChangeObservingPlayer(null);
    }
    
    public void FindAllObservedObjects()
    {
        Transform parentTransform = _stageDataManager.transform;
    
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            PlayerReferenceManager playerReferenceManager = childObject.GetComponent<PlayerReferenceManager>();

            _playersByActorNumber[playerReferenceManager.ArchievePlayerActorNumber] = childObject;
            _actorNumbersOfPlayers.Add(playerReferenceManager.ArchievePlayerActorNumber);
        }
    }

    public void OnRelease()
    {
        _compositeDisposable.Dispose();
    }
}
