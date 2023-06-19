using Model;
using UniRx;

public class ConfigPanelPresenter : Presenter
{
    private ConfigPanelView _configPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _configPanelView = view as ConfigPanelView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _configPanelView.ClosePanelButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.CurrentLobbyState.Home))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        LobbySceneModel.LobbyState
            .Where(state => state == LobbySceneModel.CurrentLobbyState.Settings)
            .Subscribe(_ => SetActiveConfigPanel(true))
            .AddTo(_compositeDisposable);
        
        LobbySceneModel.LobbyState
            .Where(state => state != LobbySceneModel.CurrentLobbyState.Settings)
            .Subscribe(_ => SetActiveConfigPanel(false))
            .AddTo(_compositeDisposable);
    }

    private void SetActiveConfigPanel(bool status)
    {
        _configPanelView.BackgroundImage.gameObject.SetActive(status);
        _configPanelView.ConfigsButton.gameObject.SetActive(status);
        _configPanelView.HowToPlayButton.gameObject.SetActive(status);
        _configPanelView.GameExitButton.gameObject.SetActive(status);
        _configPanelView.ClosePanelButton.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _configPanelView = default;
        _compositeDisposable.Dispose();
    }
}
