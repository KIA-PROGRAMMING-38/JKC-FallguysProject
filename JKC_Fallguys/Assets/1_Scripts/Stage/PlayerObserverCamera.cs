using System.Collections.Generic;
using Model;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class PlayerObserverCamera : MonoBehaviour
{
    private ObserverCamera _observerCamera;
    
    private ReactiveProperty<GameObject> _targetObject = new ReactiveProperty<GameObject>();
    
    private bool _isObservationInProgress = false;
    private readonly Dictionary<int, GameObject> _playersByActorNumber = new Dictionary<int, GameObject>();
    private readonly List<int> _actorNumbersOfPlayers = new List<int>();
    private int _currentPlayerIndexInActorNumbers = 0;

    private void Awake()
    {
        _observerCamera = transform.Find("ObserverCamera").GetComponent<ObserverCamera>();
        Debug.Assert(_observerCamera != null);
        _observerCamera.Initialize(this);

        InitializeRx();
    }

    private void FindAllObservedObjects()
    {
        Transform parentTransform = StageManager.Instance.PlayerRepository.transform;
    
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            PlayerReferenceManager playerReferenceManager = childObject.GetComponent<PlayerReferenceManager>();

            _playersByActorNumber[playerReferenceManager.ArchievePlayerActorNumber] = childObject;
            _actorNumbersOfPlayers.Add(playerReferenceManager.ArchievePlayerActorNumber);
        }
    }

    private void InitializeRx()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageManager.Instance.PlayerContainer.IsPlayerActive(actorNumber)
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => _observerCamera.gameObject.SetActive(true))
            .AddTo(this);
        
        StageManager.Instance.ObjectRepository.CurrentSequence
            .Where(sequence => sequence != ObjectRepository.StageSequence.GameInProgress)
            .Subscribe(_ => _observerCamera.gameObject.SetActive(false))
            .AddTo(this);
        
        StageManager.Instance.ObjectRepository.CurrentSequence
            .Where(sequence => sequence == ObjectRepository.StageSequence.GameInProgress)
            .First()
            .Subscribe(_ => FindAllObservedObjects())
            .AddTo(this);
        
        Observable.EveryUpdate()
            .Select(_ => _targetObject.Value != null && !_targetObject.Value.activeSelf)
            .DistinctUntilChanged()
            .Where(isInactive => isInactive)
            .Subscribe(_ => ObservedNextPlayer())
            .AddTo(this);
    }
    
    private void ChangeObservingPlayer(GameObject gameObject)
    {
        _targetObject.Value = gameObject;
        BindObservedCharacter(gameObject.transform.Find("Character"));
    }

    public void ObservedNextPlayer()
    {
        int startIndex = (_currentPlayerIndexInActorNumbers + 1) % _actorNumbersOfPlayers.Count;
        SetObservingPlayer(startIndex, true);
    }

    public void ObservedPrevPlayer()
    {
        int startIndex = (_currentPlayerIndexInActorNumbers - 1 + _actorNumbersOfPlayers.Count) % _actorNumbersOfPlayers.Count;
        SetObservingPlayer(startIndex, false);
    }

    private void SetObservingPlayer(int startIndex, bool isNext)
    {
        if (_playersByActorNumber.Count == 0 || _isObservationInProgress)
        {
            return;
        }

        _isObservationInProgress = true;
        _currentPlayerIndexInActorNumbers = startIndex;

        int playerCount = _actorNumbersOfPlayers.Count; 

        for (int i = 0; i < playerCount; i++)
        {
            GameObject current = _playersByActorNumber[_actorNumbersOfPlayers[_currentPlayerIndexInActorNumbers]];
    
            if (current != null && current.activeSelf && current.transform.Find("Character") != null)
            {
                ChangeObservingPlayer(current);
                _isObservationInProgress = false;
                return;
            }
    
            _currentPlayerIndexInActorNumbers = isNext 
                ? (_currentPlayerIndexInActorNumbers + 1) % _actorNumbersOfPlayers.Count 
                : (_currentPlayerIndexInActorNumbers - 1 + _actorNumbersOfPlayers.Count) % _actorNumbersOfPlayers.Count;
        }

        _isObservationInProgress = false;
        _targetObject.Value = null;
    }


    private void BindObservedCharacter(Transform followPlayerCharacter)
    {
        _observerCamera.UpdatePlayerTarget(followPlayerCharacter);
        PlayerReferenceManager refManager = followPlayerCharacter.parent.GetComponent<PlayerReferenceManager>();
        StageSceneModel.SetObservedPlayerActorName(refManager.ArchievePlayerNickName);
    }
}
