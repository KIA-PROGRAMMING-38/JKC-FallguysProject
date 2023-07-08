using Model;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class PlayerObserverCamera : MonoBehaviour
{
    private ObserverCamera _observerCamera;

    private void Awake()
    {
        _observerCamera = transform.Find("ObserverCamera").GetComponent<ObserverCamera>();
        Debug.Assert(_observerCamera != null);

        InitializeRx();
    }

    private void Start()
    {
        StageDataManager.Instance.PlayerContainer.BindObservingCamera(this);
    }

    private void InitializeRx()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageDataManager.Instance.IsPlayerActive(actorNumber)
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => _observerCamera.gameObject.SetActive(true))
            .AddTo(this);
        
        StageDataManager.Instance.IsGameActive
            .Where(state => !state)
            .Subscribe(_ => _observerCamera.gameObject.SetActive(false))
            .AddTo(this);
        
        StageDataManager.Instance.IsRoundCompleted
            .Where(state => state)
            .Subscribe(_ => _observerCamera.gameObject.SetActive(false))
            .AddTo(this);
    }

    public void BindObservedCharacter(Transform followPlayerCharacter)
    {
        _observerCamera.UpdatePlayerTarget(followPlayerCharacter);
        PlayerReferenceManager refManager = followPlayerCharacter.parent.GetComponent<PlayerReferenceManager>();
        StageSceneModel.SetObservedPlayerActorName(refManager.ArchievePlayerNickName);
    }
}
