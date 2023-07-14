using Model;
using UniRx;
using UnityEngine;
using Util.Helper;

public class ConfigsPresenter : Presenter
{
    private ConfigsView _configsView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    private (string resolution, int width, int height)[] _resolutions = {
        ("1920 * 1080", 1920, 1080),
        ("1600 * 900", 1600, 900),
        ("1366 * 768", 1366, 768)
    };
    private int _resolutionIndex = 0;

    public override void OnInitialize(View view)
    {
        _configsView = view as ConfigsView;
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _configsView.ResolutionLeftButton
            .OnClickAsObservable()
            .Subscribe(_ => {
                _resolutionIndex = (_resolutionIndex + 1) % _resolutions.Length;
                SetResolution(_resolutionIndex);
            })
            .AddTo(_compositeDisposable);

        _configsView.ResolutionRightButton
            .OnClickAsObservable()
            .Subscribe(_ => {
                _resolutionIndex = (_resolutionIndex - 1 + _resolutions.Length) % _resolutions.Length;
                SetResolution(_resolutionIndex);
            })
            .AddTo(_compositeDisposable);
    }

    private void SetResolution(int resolutionIndex)
    {
        (string resolution, int width, int height) resolutionSettings = _resolutions[resolutionIndex];

        switch (resolutionIndex)
        {
            case 0:
                Screen.SetResolution(resolutionSettings.width, resolutionSettings.height, FullScreenMode.FullScreenWindow);
                break;
            
            case 1:
            case 2:
                Screen.SetResolution(resolutionSettings.width, resolutionSettings.height, FullScreenMode.Windowed);
                break;
        }
        
        _configsView.ResolutionSettings.text = resolutionSettings.resolution;
    }

    protected override void OnUpdatedModel()
    {
        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_configsView.gameObject, state == LobbySceneModel.LobbyState.Configs))
            .AddTo(_compositeDisposable);
    }

    public override void OnRelease()
    {
        _configsView = default;
        _compositeDisposable.Dispose();
    }
}
