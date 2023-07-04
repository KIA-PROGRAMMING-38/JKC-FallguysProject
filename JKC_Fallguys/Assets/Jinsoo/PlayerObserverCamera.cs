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

    private void InitializeRx()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageDataManager.Instance.GetCurrentState(actorNumber)
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => _observerCamera.gameObject.SetActive(true))
            .AddTo(this);
        
        StageDataManager.Instance.GetCurrentState(actorNumber)
            .Where(state => state == StageDataManager.PlayerState.Default)
            .Subscribe(_ => _observerCamera.gameObject.SetActive(false))
            .AddTo(this);
    }

    public void BindObservedCharacter(Transform followPlayerCharacter)
    {
        _observerCamera.UpdatePlayerTarget(followPlayerCharacter);
    }

    
}
