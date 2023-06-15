using Cysharp.Threading.Tasks;
using UniRx;

public class GameLoadingMainPanelPresenter : Presenter
{
    private GameLoadingMainPanelView _gameLoadingMainPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _gameLoadingMainPanelView = view as GameLoadingMainPanelView;

        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        Model.GameLoadingSceneModel.IsLoadingSceneSwitch
            .Where(isActive => !isActive)
            .Subscribe(_ => SetActiveGameObject(false))
            .AddTo(_compositeDisposable);
    }
    
    private void SetActiveGameObject(bool status)
    {
        _gameLoadingMainPanelView.Default.SetActive(status);
        _gameLoadingMainPanelView.Mask.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _gameLoadingMainPanelView = default;
        _compositeDisposable.Dispose();
    }
}
