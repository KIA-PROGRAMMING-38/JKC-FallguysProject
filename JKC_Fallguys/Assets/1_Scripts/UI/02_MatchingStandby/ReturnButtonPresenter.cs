using UniRx;

public class ReturnButtonPresenter : Presenter
{
    private ReturnButtonView _returnButtonView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _returnButtonView = view as ReturnButtonView;
        // 룸에 입장할 경우의 변수 초기화.
        Model.MatchingSceneModel.PossibleToEnter(true);
        
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
            .ObserveEveryValueChanged(_ => Model.MatchingSceneModel.IsEnterLobbyFromMatchingScene)
            .Where(_ => Model.MatchingSceneModel.IsEnterLobbyFromMatchingScene)
            .Where(_ => Model.MatchingSceneModel.IsActionPossible)
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