using UniRx;

namespace Model
{
    public static class StageSceneModel
    {
        // 패널의 활성화 여부를 결정하는 불리언 값입니다.
        private static ReactiveProperty<bool> _isExitPanelPopUp = new ReactiveProperty<bool>(false);
        public static IReadOnlyReactiveProperty<bool> IsExitPanelPopUp => _isExitPanelPopUp;
        
        private static ReactiveProperty<bool> _isEnterPhotonRoom = new ReactiveProperty<bool>(true);
        public static IReadOnlyReactiveProperty<bool> IsEnterPhotonRoom => _isEnterPhotonRoom;

        private static ReactiveProperty<bool> _canClickButton = new ReactiveProperty<bool>(false);
        public static IReadOnlyReactiveProperty<bool> CanClickButton => _canClickButton;
        
        private static ReactiveProperty<int> _spriteIndex = new ReactiveProperty<int>();
        public static IReactiveProperty<int> SpriteIndex => _spriteIndex;

        private static ReactiveProperty<int> _remainingTime = new ReactiveProperty<int>();
        public static IReactiveProperty<int> RemainingTime => _remainingTime;

        private static ReactiveProperty<string> _observedPlayerActorName = new ReactiveProperty<string>();
        public static IReactiveProperty<string> ObservedPlayerActorName => _observedPlayerActorName; 

        /// <summary>
        /// Exit 패널이 활성화 될 수 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="status"></param>
        public static void SetExitPanelActive(bool status)
        {
            _isExitPanelPopUp.Value = status;
        }

        /// <summary>
        /// Exit 버튼이 활성화 될 수 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="status"></param>
        public static void SetExitButtonActive(bool status)
        {
            _canClickButton.Value = status;
        }
        
        /// <summary>
        /// 현재 포톤 룸에 입장했음을 알려줄 수 있는 함수입니다.
        /// </summary>
        /// <param name="status">입장했을 경우 true, 나갈 경우 false를 인수로 넘겨줍니다.</param>
        public static void RoomAdmissionStatus(bool status)
        {
            _isEnterPhotonRoom.Value = status;
        }

        public static void SetRemainingTime(int remainingTime)
        {
            _remainingTime.Value = remainingTime;
        }

        public static void DecreaseRemainingTime()
        {
            --_remainingTime.Value;
        }
        
        public static void IncreaseCountDownIndex()
        {
            ++_spriteIndex.Value;
        }

        public static void InitializeCountDown()
        {
            _spriteIndex.Value = 0;
        }

        public static void SetObservedPlayerActorName(string actorName)
        {
            _observedPlayerActorName.Value = actorName;
        }
    }
}