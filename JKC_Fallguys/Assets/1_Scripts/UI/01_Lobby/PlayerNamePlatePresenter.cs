using Photon.Pun;
using UniRx;

public class PlayerNamePlatePresenter : Presenter
{
    private PlayerNamePlateView _playerNamePlateView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public override void OnInitialize(View view)
    {
        _playerNamePlateView = view as PlayerNamePlateView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        UpdatePlayerDataModel(PhotonNetwork.NickName);
    }

    private void UpdatePlayerDataModel(string playerName)
    {
        Model.LobbySceneModel.SetPlayerName(playerName);
    }

    protected override void OnUpdatedModel()
    {
        UpdateNamePlate();
        
        Model.LobbySceneModel.CurrentLobbyState
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_playerNamePlateView.gameObject, state == Model.LobbySceneModel.LobbyState.Home))
            .AddTo(_compositeDisposable);
    } 
    
    private void UpdateNamePlate()
    {
        _playerNamePlateView.PlayerNameText.text = Model.LobbySceneModel.PlayerName.Value;
    }
    
    public override void OnRelease()
    {
        _playerNamePlateView = default;
        _compositeDisposable.Dispose();
    }
}
