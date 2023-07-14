using UniRx;
using Util.Helper;

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
            .Subscribe(_ => SetData())
            .AddTo(_compositeDisposable);
    }

    private void SetData()
    {
        GameObjectHelper.SetActiveGameObject(_mapInformationView.gameObject, true);
        
        MapData mapData = StageManager.Instance.StageDataManager.MapDatas[StageManager.Instance.StageDataManager.MapPickupIndex.Value];
        
        _mapInformationView.MapNameText.text = mapData.Info.MapName;
        _mapInformationView.MapSplashArtImage.sprite =
            SplashArtRegistry.SpriteArts[mapData.Info.SplashArtRegistryIndex];
        _mapInformationView.PlayExplanation.text = mapData.Info.Description;
    }
    
    public override void OnRelease()
    {
        _mapInformationView = default;
        _compositeDisposable.Dispose();
    }
}
