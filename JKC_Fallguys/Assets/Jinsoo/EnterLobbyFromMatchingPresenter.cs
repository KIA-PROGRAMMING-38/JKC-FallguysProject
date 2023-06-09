using UniRx;

public class EnterLobbyFromMatchingPresenter : Presenter
{
    private EnterLobbyFromMatchingView _enterLobbyFromMatchingView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterLobbyFromMatchingView = view as EnterLobbyFromMatchingView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _enterLobbyFromMatchingView.CancelButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.MatchingSceneModel.DeActiveEnterLobbyPanel())
            .AddTo(_compositeDisposable);

        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MatchingSceneModel.IsActionPossible)
            .Where(_ => !Model.MatchingSceneModel.IsActionPossible)
            .Subscribe(_ => Model.MatchingSceneModel.DeActiveEnterLobbyPanel())
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MatchingSceneModel.IsEnterLobbyFromMatchingScene)
            .Where(_ => !Model.MatchingSceneModel.IsEnterLobbyFromMatchingScene)
            .Subscribe(_ => DeActivateEnterLobbyPanel())
            .AddTo(_compositeDisposable);
    }
    
    private void DeActivateEnterLobbyPanel()
    {
        _enterLobbyFromMatchingView.EnterLobbyFromMatchingViewController.gameObject.SetActive(false);
    }
    
    public override void OnRelease()
    {
        _enterLobbyFromMatchingView = default;
        _compositeDisposable.Dispose();
    }
}
