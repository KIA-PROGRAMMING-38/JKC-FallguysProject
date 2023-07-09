using Model;
using UniRx;

public class ConfigReturnButtonPresenter : Presenter
{
    private ConfigReturnButtonView _configReturnButtonView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _configReturnButtonView = view as ConfigReturnButtonView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _configReturnButtonView.ActionButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Settings))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => SetActiveObject(state == LobbySceneModel.LobbyState.Configs))
            .AddTo(_compositeDisposable);
    }

     private void SetActiveObject(bool status)
    {
        _configReturnButtonView.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _configReturnButtonView = default;
        _compositeDisposable.Dispose();
    }
}
