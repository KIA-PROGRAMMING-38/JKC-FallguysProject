using Model;
using UniRx;

public class SettingsPanelPresenter : Presenter
{
    private SettingsPanelView _settingsPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _settingsPanelView = view as SettingsPanelView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _settingsPanelView.ConfigsButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Configs))
            .AddTo(_compositeDisposable);

        _settingsPanelView.HowToPlayButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.HowToPlay))
            .AddTo(_compositeDisposable);

        _settingsPanelView.ClosePanelButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Home))
            .AddTo(_compositeDisposable);

        _settingsPanelView.GameExitButton.onClick.AddListener(GameExit);
    }

    private void GameExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    protected override void OnUpdatedModel()
    {
        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_settingsPanelView.gameObject, state == LobbySceneModel.LobbyState.Settings))
            .AddTo(_compositeDisposable);

        // Config Panel이 활성화 됐을때 선택될 버튼을 정합니다.
        LobbySceneModel.CurrentLobbyState
            .Where(state => state == LobbySceneModel.LobbyState.Settings)
            .Subscribe(_ => _settingsPanelView.HowToPlayButton.Select())
            .AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _settingsPanelView = default;
        _compositeDisposable.Dispose();
    }
}
