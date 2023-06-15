using UniRx;

public class MapInformationPresenter : Presenter
{
    private MapInformationView _mapInformationView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _mapInformationView = view as MapInformationView;
        
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
            .Subscribe(_ => SetActiveGameObject(true))
            .AddTo(_compositeDisposable);
    }

    private void SetActiveGameObject(bool status)
    {
        _mapInformationView.Default.SetActive(status);
        _mapInformationView.MapNameText.gameObject.SetActive(status);
        _mapInformationView.MapSplashArtMask.gameObject.SetActive(status);
        _mapInformationView.PlayExplanation.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _mapInformationView = default;
        _compositeDisposable.Dispose();
    }
}
