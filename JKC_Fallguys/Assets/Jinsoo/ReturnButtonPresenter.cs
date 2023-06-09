using UniRx;

public class ReturnButtonPresenter : Presenter
{
    private ReturnButtonView _returnButtonView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _returnButtonView = view as ReturnButtonView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _returnButtonView.UIPopUpButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.MatchingSceneModel.ActiveEnterLobbyPanel())
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MatchingSceneModel.IsEnterLobbyFromMatching)
            .Where(_ => Model.MatchingSceneModel.IsEnterLobbyFromMatching)
            .Subscribe(_ => OnActiveEnterLobbyPanel())
            .AddTo(_compositeDisposable);
    }

    private void OnActiveEnterLobbyPanel()
    {
        _returnButtonView.EnterLobbyFromMatchingViewController.gameObject.SetActive(true);
    }
    
    public override void OnRelease()
    {
        _returnButtonView = default;
        _compositeDisposable.Dispose();
    }
}