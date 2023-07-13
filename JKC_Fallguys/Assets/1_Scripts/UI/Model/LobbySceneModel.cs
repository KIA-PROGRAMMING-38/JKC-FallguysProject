using UniRx;

namespace Model
{
    public static class LobbySceneModel
    {
        public enum LobbyState
        {
            Default,
            Home,
            Customization,
            Costume,
            Settings,
            HowToPlay,
            Configs
        }
        
        private static ReactiveProperty<string> _playerName = new ReactiveProperty<string>();
        public static IReadOnlyReactiveProperty<string> PlayerName => _playerName;

        private static ReactiveProperty<LobbyState> _currentLobbyState = 
            new ReactiveProperty<LobbyState>(LobbyState.Default);
        public static IReadOnlyReactiveProperty<LobbyState> CurrentLobbyState => _currentLobbyState;

        private static ReactiveProperty<int> _howToPlayImageIndex = new ReactiveProperty<int>(0);
        public static IReadOnlyReactiveProperty<int> HowToPlayImageIndex => _howToPlayImageIndex;

        private static ReactiveProperty<string> _costumeColorName = new ReactiveProperty<string>();
        public static IReactiveProperty<string> CostumeColorName => _costumeColorName;
        
        private static ReactiveProperty<int> _playerTextureIndex = new ReactiveProperty<int>();
        public static readonly IReactiveProperty<int> PlayerTextureIndex = _playerTextureIndex;

        private static ReactiveProperty<bool> _isConnectedToPhotonLobby = new ReactiveProperty<bool>(false );
        public static IReactiveProperty<bool> IsConnectedToPhotonLobby => _isConnectedToPhotonLobby;

        public static void IncreaseImageIndex()
        {
            ++_howToPlayImageIndex.Value;
        }

        public static void ResetImageIndex()
        {
            _howToPlayImageIndex.Value = 0;
        }

        public static void SetPlayerName(string playerName)
        {
            _playerName.Value = playerName;
        }

        public static void SetLobbyState(LobbyState lobbyState)
        {
            _currentLobbyState.Value = lobbyState;
        }

        public static void SetColorName(string colorName)
        {
            _costumeColorName.Value = colorName;
        }
        
        public static void SetPlayerTexture(int index)
        {
            _playerTextureIndex.Value = index;
        }
        
        public static void SetPhotonLobbyWarmingPanelState(bool status)
        {
            _isConnectedToPhotonLobby.Value = status;
        }
    }
}