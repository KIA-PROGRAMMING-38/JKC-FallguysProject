using Model;
using UniRx;
using UnityEngine;

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
        _settingsPanelView.ClosePanelButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Home))
            .AddTo(_compositeDisposable);

        _settingsPanelView.HowToPlayButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.HowToPlay))
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
            .Where(state => state == LobbySceneModel.LobbyState.Settings)
            .Subscribe(_ => SetActiveSettingsPanel(true))
            .AddTo(_compositeDisposable);
        
        LobbySceneModel.CurrentLobbyState
            .Where(state => state != LobbySceneModel.LobbyState.Settings)
            .Subscribe(_ => SetActiveSettingsPanel(false))
            .AddTo(_compositeDisposable);
        
        // Config Panel이 활성화 됐을때 선택될 버튼을 정합니다.
        LobbySceneModel.CurrentLobbyState
            .Where(state => state == LobbySceneModel.LobbyState.Settings)
            .Subscribe(_ => _settingsPanelView.HowToPlayButton.Select())
            .AddTo(_compositeDisposable);
    }

    private void SetActiveSettingsPanel(bool status)
    {
        _settingsPanelView.BackgroundImage.gameObject.SetActive(status);
        _settingsPanelView.ConfigsButton.gameObject.SetActive(status);
        _settingsPanelView.HowToPlayButton.gameObject.SetActive(status);
        _settingsPanelView.GameExitButton.gameObject.SetActive(status);
        _settingsPanelView.ClosePanelButton.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _settingsPanelView = default;
        _compositeDisposable.Dispose();
    }
}
