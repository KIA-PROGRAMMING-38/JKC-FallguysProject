using Model;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class ObservedPlayerNamePresenter : Presenter
{
    private ObservedPlayerNameView _observedPlayerNameView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _observedPlayerNameView = view as ObservedPlayerNameView;

        SetObservedPlayerName();
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        StageDataManager.Instance.IsGameActive
            .DistinctUntilChanged()
            .Subscribe(_ => SetActiveGameObject(false))
            .AddTo(_compositeDisposable);
        
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageDataManager.Instance.GetCurrentState(actorNumber)
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => SetActiveGameObject(true))
            .AddTo(_compositeDisposable);

        StageSceneModel.ObservedPlayerActorName
            .DistinctUntilChanged()
            .Subscribe(_ => SetObservedPlayerName())
            .AddTo(_compositeDisposable);
    }

    private void SetObservedPlayerName()
    {
        _observedPlayerNameView.PlayerNameText.text = StageSceneModel.ObservedPlayerActorName.Value;
    }

    private void SetActiveGameObject(bool status)
    {
        _observedPlayerNameView.Default.SetActive(status);
        _observedPlayerNameView.PlayerNameText.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _observedPlayerNameView = default;
        _compositeDisposable.Dispose();
    }
}
