using UniRx;
using UniRx.Triggers;

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
        Model.GameLoadingSceneModel.IsLoadingSceneSwitch
            .Where(isActive => !isActive)
            .Subscribe(_ => SetActiveGameObject(true))
            .AddTo(_compositeDisposable);

        _mapInformationView.MapNameText
            .OnEnableAsObservable()
            .Subscribe(_ => SetData())
            .AddTo(_compositeDisposable);
    }

    private void SetData()
    {
        MapData mapData = StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex];

        _mapInformationView.MapNameText.text = mapData.Info.MapName;
        _mapInformationView.MapSplashArtImage.sprite =
            SplashArtRegistry.SpriteArts[mapData.Info.SplashArtRegistryIndex];
        _mapInformationView.PlayExplanation.text = mapData.Info.Description;
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
