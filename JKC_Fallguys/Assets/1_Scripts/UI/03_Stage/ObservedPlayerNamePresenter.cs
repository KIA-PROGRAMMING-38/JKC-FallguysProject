using Model;
using Photon.Pun;
using UniRx;

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
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        StageDataManager.Instance.IsGameActive
            .DistinctUntilChanged()
            .Subscribe(gameActive => 
            {
                if (!gameActive) SetActiveGameObject(false);
            })
            .AddTo(_compositeDisposable);
        
        StageDataManager.Instance.PlayerContainer.GetCurrentState(actorNumber)
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => 
            {
                if (StageDataManager.Instance.IsGameActive.Value)
                    SetActiveGameObject(true);
            })
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
