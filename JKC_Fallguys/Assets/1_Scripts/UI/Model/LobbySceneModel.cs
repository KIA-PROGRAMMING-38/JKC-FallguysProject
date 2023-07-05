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
        
        // PlayerNamePlate의 갱신을 담당할 문자열입니다.
        private static ReactiveProperty<string> _playerName = new ReactiveProperty<string>();
        public static IReadOnlyReactiveProperty<string> PlayerName => _playerName;

        // 현재 로비 화면의 상태를 알려줄 데이터입니다.
        private static ReactiveProperty<LobbyState> _currentLobbyState = 
            new ReactiveProperty<LobbyState>(LobbyState.Default);
        public static IReadOnlyReactiveProperty<LobbyState> CurrentLobbyState => _currentLobbyState;

        // How To Play의 활성화를 나타낼 Image의 인덱스를 나타내는 변수입니다.
        private static ReactiveProperty<int> _howToPlayImageIndex = new ReactiveProperty<int>(0);
        public static IReadOnlyReactiveProperty<int> HowToPlayImageIndex => _howToPlayImageIndex;

        private static ReactiveProperty<string> _costumeColorName = new ReactiveProperty<string>();
        public static IReactiveProperty<string> CostumeColorName => _costumeColorName;

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
    }
}