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

        StageManager.Instance.StageDataManager.IsGameActive
            .DistinctUntilChanged()
            .Subscribe(gameActive => 
            {
                if (!gameActive) 
                    GameObjectHelper.SetActiveGameObject(_observedPlayerNameView.gameObject, false);
            })
            .AddTo(_compositeDisposable);
        
        StageManager.Instance.PlayerContainer.GetCurrentState(actorNumber)
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => 
            {
                if (StageManager.Instance.StageDataManager.IsGameActive.Value)
                    GameObjectHelper.SetActiveGameObject(_observedPlayerNameView.gameObject, true);
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
    
    public override void OnRelease()
    {
        _observedPlayerNameView = default;
        _compositeDisposable.Dispose();
    }
}
