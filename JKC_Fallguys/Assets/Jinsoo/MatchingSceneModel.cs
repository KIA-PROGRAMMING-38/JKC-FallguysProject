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
        
        // 게임 시작까지 카운트다운 숫자를 의미하는 객체입니다.
        private static int _startCount;
        public static int StartCount
        {
            get { return _startCount; }
        }

        /// <summary>
        /// IsEnterLobbyFromMatching을 true로 변환합니다.
        /// true로 바뀌는 순간 패널이 활성화됩니다.
        /// </summary>
        public static void ActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatching = true;
        }

        /// <summary>
        /// IsEnterLobbyFromMatching을 false로 변환합니다.
        /// false로 바뀌는 순간 패널이 비활성화됩니다.
        /// </summary>
        public static void DeActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatching = false;
        }
        
        /// <summary>
        /// StartCount를 10으로 세팅합니다.
        /// </summary>
        public static void ResetStartCount()
        {
            _startCount = 10;
        }

        /// <summary>
        /// 호출 될 때마다 StartCount를 1씩 감소시킵니다.
        /// </summary>
        public static void DecreaseStartCount()
        {
            --_startCount;
        }
    }
}