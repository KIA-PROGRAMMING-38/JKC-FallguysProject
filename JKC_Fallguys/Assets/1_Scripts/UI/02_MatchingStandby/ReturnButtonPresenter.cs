using UniRx;

public class ReturnButtonPresenter : Presenter
{
    private ReturnButtonView _returnButtonView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _returnButtonView = view as ReturnButtonView;
        // 룸에 입장할 경우의 변수 초기화.
        Model.MatchingSceneModel.PossibleToExit(true);
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _returnButtonView.UIPopUpButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.MatchingSceneModel.SetActiveEnterLobbyPanel(true))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
    }

    public override void OnRelease()
    {
        _returnButtonView = default;
        _compositeDisposable.Dispose();
    }
}