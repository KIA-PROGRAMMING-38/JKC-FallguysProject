using UniRx;

namespace Model
{
    public static class MatchingSceneModel
    {
        // 패널 활성화 여부를 결정하는 불리언 값입니다
        private static ReactiveProperty<bool> _isEnterLobbyFromMatchingScene = new ReactiveProperty<bool>();
        public static IReadOnlyReactiveProperty<bool> IsEnterLobbyFromMatchingScene => _isEnterLobbyFromMatchingScene;

        // 포톤 룸에 입장했음을 알리는 불리언 값입니다.
        private static ReactiveProperty<bool> _isEnterPhotonRoom = new ReactiveProperty<bool>();
        public static IReadOnlyReactiveProperty<bool> IsEnterPhotonRoom => _isEnterPhotonRoom;
        
        // 패널 조작이 가능한지를 결정하는 불리언 값입니다.
        private static ReactiveProperty<bool> _isActionPossible = new ReactiveProperty<bool>();
        public static IReadOnlyReactiveProperty<bool> IsActionPossible => _isActionPossible;

        // 게임 시작까지 카운트다운 할 숫자를 나타냅니다.
        private static ReactiveProperty<int> _startCount = new ReactiveProperty<int>();
        public static IReadOnlyReactiveProperty<int> StartCount => _startCount;

        /// <summary>
        /// IsEnterLobbyFromMatching 값을 true로 설정합니다.
        /// 이 때, 패널이 활성화됩니다.
        /// </summary>
        public static void ActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatchingScene.Value = true;
        }

        /// <summary>
        /// IsEnterLobbyFromMatching 값을 false로 설정합니다.
        /// 이 때, 패널이 비활성화됩니다.
        /// </summary>
        public static void DeActiveEnterLobbyPanel()
        {
            _isEnterLobbyFromMatchingScene.Value = false;
        }
        
        /// <summary>
        /// StartCount를 10으로 재설정합니다.
        /// </summary>
        public static void ResetStartCount()
        {
            _startCount.Value = 10;
        }

        /// <summary>
        /// 매 호출시마다 StartCount 값을 1씩 감소시킵니다..
        /// </summary>
        public static void DecreaseStartCount()
        {
            --_startCount.Value;
        }

        /// <summary>
        /// 현재 포톤 룸에 입장했음을 알려줄 수 있는 함수입니다.
        /// </summary>
        /// <param name="status">입장했을 경우 true, 나갈 경우 false를 인수로 넘겨줍니다.</param>
        public static void RoomAdmissionStatus(bool status)
        {
            _isEnterPhotonRoom.Value = status;
        }

        /// <summary>
        /// 현재 조작이 가능한지 여부를 결정할 수 있는 함수입니다.
        /// </summary>
        /// <param name="status">true일 경우 UI와의 상호작용이 가능하며, false일 경우 불가능합니다.</param>
        public static void PossibleToEnter(bool status)
        {
            _isActionPossible.Value = status;
        }
    }
}