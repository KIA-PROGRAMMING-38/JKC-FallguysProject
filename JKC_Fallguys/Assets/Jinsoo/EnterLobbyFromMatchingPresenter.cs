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
    }

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MatchingSceneModel.IsEnterLobbyFromMatching)
            .Where(_ => !Model.MatchingSceneModel.IsEnterLobbyFromMatching)
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
