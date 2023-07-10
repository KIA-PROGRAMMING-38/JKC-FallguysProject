using LiteralRepository;
using UniRx;

public class EnterMatchingPresenter : Presenter
{
    private EnterMatchingView _enterMatchingView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterMatchingView = view as EnterMatchingView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _enterMatchingView.EnterMatchingButton
            .OnClickAsObservable()
            .Subscribe(_ => TryEnterMatchingStandby())
            .AddTo(_compositeDisposable);
    }

    private void TryEnterMatchingStandby()
    {
        SceneChangeHelper.ChangeLocalScene(SceneIndex.MatchingStandby);
    }

    protected override void OnUpdatedModel()
    {
        Model.LobbySceneModel.CurrentLobbyState
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_enterMatchingView.gameObject, state == Model.LobbySceneModel.LobbyState.Home))
            .AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _enterMatchingView = default;
        _compositeDisposable.Dispose();
    }
}
