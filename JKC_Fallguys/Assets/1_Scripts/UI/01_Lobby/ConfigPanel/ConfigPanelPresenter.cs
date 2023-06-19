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
            .Subscribe(_ => Model.LobbySceneModel.SetActiveConfigView(false))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        Model.LobbySceneModel.IsConfigurationRunning
            .Subscribe(isRunning => SetActiveConfigPanel(isRunning))
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
