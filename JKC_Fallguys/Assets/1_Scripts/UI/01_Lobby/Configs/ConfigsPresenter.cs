using Model;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ConfigsPresenter : Presenter
{
    private ConfigsView _configsView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _configsView = view as ConfigsView;

        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
    }

    protected override void OnUpdatedModel()
    {
        var ConfigsState = LobbySceneModel.LobbyState.Configs;

        // Configs UI의 활성화 여부를 결정합니다.
        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => SetActiveConfigs(state == ConfigsState))
            .AddTo(_compositeDisposable);
    }

    private void SetActiveConfigs(bool status)
    {
        _configsView.gameObject.SetActive(status);
    }

    public override void OnRelease()
    {
        _configsView = default;
        _compositeDisposable.Dispose();
    }
}
