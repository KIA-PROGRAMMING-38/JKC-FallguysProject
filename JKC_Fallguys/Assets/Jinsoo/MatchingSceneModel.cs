namespace Model
{
    public static class MatchingSceneModel
    {
        // 패널 활성화 여부를 결정하는 불리언 값입니다.
        private static bool _isEnterLobbyFromMatching;
        public static bool IsEnterLobbyFromMatching
        {
            get { return _isEnterLobbyFromMatching; }
        }
        
        // 게임 시작까지 카운트다운 할 숫자를 나타냅니다.
        private static int _startCount;
        public static int StartCount
        {
            get { return _startCount; }
        }

        /// <summary>
        /// IsEnterLobbyFromMatching 값을 true로 설정합니다.
        /// 이 때, 패널이 활성화됩니다.
        /// </summary>
        public static void ActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatching = true;
        }

        /// <summary>
        /// IsEnterLobbyFromMatching 값을 false로 설정합니다.
        /// 이 때, 패널이 비활성화됩니다.
        /// </summary>
        public static void DeActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatching = false;
        }
        
        /// <summary>
        /// StartCount를 10으로 재설정합니다.
        /// </summary>
        public static void ResetStartCount()
        {
            _startCount = 10;
        }

        /// <summary>
        /// 매 호출시마다 StartCount 값을 1씩 감소시킵니다..
        /// </summary>
        public static void DecreaseStartCount()
        {
            --_startCount;
        }
    }
}