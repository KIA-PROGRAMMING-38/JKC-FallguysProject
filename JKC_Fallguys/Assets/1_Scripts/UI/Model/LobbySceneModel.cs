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
            Settings,
            HowToPlay
        }
        
        // PlayerNamePlate의 갱신을 담당할 문자열입니다.
        private static ReactiveProperty<string> _playerName = new ReactiveProperty<string>();
        public static IReadOnlyReactiveProperty<string> PlayerName => _playerName;

        // 현재 로비 화면의 상태를 알려줄 데이터입니다.
        private static ReactiveProperty<LobbyState> _currentLobbyState = 
            new ReactiveProperty<LobbyState>(LobbyState.Default);
        public static IReadOnlyReactiveProperty<LobbyState> CurrentLobbyState => _currentLobbyState;
        
        
        public static void SetPlayerName(string playerName)
        {
            _playerName.Value = playerName;
        }

        public static void SetLobbyState(LobbyState lobbyState)
        {
            _currentLobbyState.Value = lobbyState;
        }
    }
}