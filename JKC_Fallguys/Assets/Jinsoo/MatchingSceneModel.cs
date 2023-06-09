namespace Model
{
    public static class MatchingSceneModel
    {
        // bool 타입의 객체의 값에 따라 패널을 활성화, 비활성화 할지의 여부를 가리는 객체.
        private static bool _isEnterLobbyFromMatching;
        public static bool IsEnterLobbyFromMatching
        {
            get { return _isEnterLobbyFromMatching; }
        }

        public static void ActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatching = true;
        }

        public static void DeActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatching = false;
        }
    }
}