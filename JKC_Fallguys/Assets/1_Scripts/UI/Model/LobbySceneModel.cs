namespace Model
{
    public static class LobbySceneModel
    {
        public enum CurrentLobbyState
        {
            Default,
            Home,
            Customization
        }
        
        // PlayerNamePlate의 갱신을 담당할 문자열입니다.
        private static string _playerName = default;
        public static string PlayerName
        {
            get { return _playerName; }
        }

        // 현재 로비 화면의 상태를 알려줄 데이터입니다.
        private static CurrentLobbyState _lobbyState = CurrentLobbyState.Default;
        public static CurrentLobbyState LobbyState
        {
            get { return _lobbyState; }
        }

        // 환경설정에 들어간지 판단하는 데이터입니다.
        private static bool _isConfigurationRunning = false;
        public static bool IsConfigurationRunning
        {
            get { return _isConfigurationRunning; }
        }
        
        public static void SetPlayerName(string playerName)
        {
            _playerName = playerName;
        }

        public static void SetLobbyState(CurrentLobbyState lobbyState)
        {
            _lobbyState = lobbyState;
        }

        public static void SetActiveConfigView(bool configurationState)
        {
            _isConfigurationRunning = configurationState;
        }
    }
}