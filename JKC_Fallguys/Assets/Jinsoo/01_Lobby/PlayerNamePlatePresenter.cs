using Photon.Pun;

public class PlayerNamePlatePresenter : Presenter
{
    private PlayerNamePlateView _playerNamePlateView;
    
    public override void OnInitialize(View view)
    {
        _playerNamePlateView = view as PlayerNamePlateView;
        
        // 이 클래스는 UniRx를 사용하지 않으며, InitializeRx 메서드는 구현되지 않았습니다.
        // 그러나 코드베이스 전반에 걸쳐 일관된 코드 스타일과 컨벤션을 유지하기 위해,
        // 여기에 InitializeRx 메서드의 플레이스홀더를 유지하였습니다.
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        UpdatePlayerDataModel(PhotonNetwork.NickName);
    }

    private void UpdatePlayerDataModel(string playerName)
    {
        Model.LobbyDataModel.SetPlayerName(playerName);
    }

    protected override void OnUpdatedModel()
    {
        UpdateNamePlate();
    }

    private void UpdateNamePlate()
    {
        _playerNamePlateView.PlayerNameText.text = Model.LobbyDataModel.PlayerName;
    }
    
    public override void OnRelease()
    {
        _playerNamePlateView = default;
    }
}
