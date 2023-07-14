using Model;
using UniRx;
using Util.Helper;

public class LobbyConnectionWarningPanelPresenter : Presenter
{
    private LobbyConnectionWarningPanelView _lobbyConnectionWarningPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _lobbyConnectionWarningPanelView = view as LobbyConnectionWarningPanelView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _lobbyConnectionWarningPanelView.InformationTouchPanel
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetPhotonLobbyWarmingPanelState(false))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        LobbySceneModel.IsConnectedToPhotonLobby
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_lobbyConnectionWarningPanelView.gameObject, state))
            .AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _lobbyConnectionWarningPanelView = default;
        _compositeDisposable.Dispose();
    }
}
