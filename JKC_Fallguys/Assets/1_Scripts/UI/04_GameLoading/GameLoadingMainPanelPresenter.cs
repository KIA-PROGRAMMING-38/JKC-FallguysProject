using UniRx;
using Util.Helper;

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
            .Subscribe(_ => GameObjectHelper.SetActiveGameObject(_gameLoadingMainPanelView.gameObject, false))
            .AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _gameLoadingMainPanelView = default;
        _compositeDisposable.Dispose();
    }
}
