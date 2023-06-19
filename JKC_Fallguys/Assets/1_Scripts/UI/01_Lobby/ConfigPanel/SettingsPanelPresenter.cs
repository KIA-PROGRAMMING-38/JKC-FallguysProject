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
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.CurrentLobbyState.Home))
            .AddTo(_compositeDisposable);

        _settingsPanelView.HowToPlayButton
            .OnClickAsObservable()
            .Subscribe(_ => Debug.Log("How To Play"));
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
        
        // Config Panel이 활성화 됐을때 선택될 버튼을 정합니다.
        LobbySceneModel.LobbyState
            .Where(state => state == LobbySceneModel.CurrentLobbyState.Settings)
            .Subscribe(_ => _settingsPanelView.HowToPlayButton.Select())
            .AddTo(_compositeDisposable);
    }

    private void SetActiveConfigPanel(bool status)
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
