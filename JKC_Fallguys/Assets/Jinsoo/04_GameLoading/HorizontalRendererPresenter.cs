using UniRx;

public class HorizontalRendererPresenter : Presenter
{
    private HorizontalRendererView _horizontalRendererView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _horizontalRendererView = view as HorizontalRendererView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.GameLoadingSceneModel.IsLoadingSceneSwitch)
            .Where(_ => !Model.GameLoadingSceneModel.IsLoadingSceneSwitch)
            .Subscribe(_ => SetActiveGameObject(false))
            .AddTo(_compositeDisposable);
    }

    private void SetActiveGameObject(bool status)
    {
        _horizontalRendererView.SplashArtPooler.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _horizontalRendererView = default;
        _compositeDisposable.Dispose();
    }
}
