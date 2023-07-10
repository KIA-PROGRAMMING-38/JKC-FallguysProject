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
        Model.GameLoadingSceneModel.IsLoadingSceneSwitch
            .Where(isActive => !isActive)
            .Subscribe(_ => GameObjectHelper.SetActiveGameObject(_horizontalRendererView.SplashArtPooler, false))
            .AddTo(_compositeDisposable);
    }

    public override void OnRelease()
    {
        _horizontalRendererView = default;
        _compositeDisposable.Dispose();
    }
}
