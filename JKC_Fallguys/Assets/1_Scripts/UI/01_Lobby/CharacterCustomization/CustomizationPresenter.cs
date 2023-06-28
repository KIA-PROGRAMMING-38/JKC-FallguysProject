using System.Collections;
using System.Collections.Generic;
using Model;
using UniRx;
using UnityEngine;

public class CustomizationPresenter : Presenter
{
    private CustomizationView _customizationView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _customizationView = view as CustomizationView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _customizationView.Costume
            .OnClickAsObservable()
            .Subscribe(_ => Model.LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Costume));
    }

    protected override void OnUpdatedModel()
    {
        Model.LobbySceneModel.CurrentLobbyState
            .Where(state => state == Model.LobbySceneModel.LobbyState.Customization)
            .Subscribe(_ => SetActiveGameObject(true))
            .AddTo(_compositeDisposable);
        
        Model.LobbySceneModel.CurrentLobbyState
            .Where(state => state != Model.LobbySceneModel.LobbyState.Customization)
            .Subscribe(_ => SetActiveGameObject(false))
            .AddTo(_compositeDisposable);
    }

    private void SetActiveGameObject(bool status)
    {
        _customizationView.Default.SetActive(status);
        _customizationView.Costume.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _customizationView = default;
        _compositeDisposable.Dispose();
    }
}
