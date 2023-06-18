using System.Collections;
using System.Collections.Generic;
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
        
    }

    protected override void OnUpdatedModel()
    {
        Model.LobbySceneModel.LobbyState
            .Where(state => state == Model.LobbySceneModel.CurrentLobbyState.Customization)
            .Subscribe(_ => SetActiveGameObject(true))
            .AddTo(_compositeDisposable);
        
        Model.LobbySceneModel.LobbyState
            .Where(state => state != Model.LobbySceneModel.CurrentLobbyState.Customization)
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
