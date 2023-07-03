using UniRx;

namespace Model
{
    public static class StageSceneModel
    {
        // 패널의 활성화 여부를 결정하는 불리언 값입니다.
        private static ReactiveProperty<bool> _isExitPanelPopUp = new ReactiveProperty<bool>(false);
        public static IReadOnlyReactiveProperty<bool> IsExitPanelPopUp => _isExitPanelPopUp;
        
        // 포톤 룸에 입장했음을 알리는 불리언 값입니다.
        private static ReactiveProperty<bool> _isEnterPhotonRoom = new ReactiveProperty<bool>(true);
        public static IReadOnlyReactiveProperty<bool> IsEnterPhotonRoom => _isEnterPhotonRoom;

        // Exit Button을 누를 수 있는지를 결정하는 불리언 값입니다.
        private static ReactiveProperty<bool> _canClickButton = new ReactiveProperty<bool>(false);
        public static IReadOnlyReactiveProperty<bool> CanClickButton => _canClickButton;
        
        // 현재 골에 들어간 플레이어의 수를 나타냅니다.
        private static ReactiveProperty<int> _enteredGoalPlayerCount = new ReactiveProperty<int>(0);
        public static IReadOnlyReactiveProperty<int> EnteredGoalPlayerCount => _enteredGoalPlayerCount;

        // Stage에 입장한 플레이어의 수를 나타냅니다.
        private static ReactiveProperty<int> _totalPlayerCount = new ReactiveProperty<int>(0);
        public static IReadOnlyReactiveProperty<int> TotalPlayerCount => _totalPlayerCount;

        private static ReactiveProperty<int> _spriteIndex = new ReactiveProperty<int>();
        public static IReactiveProperty<int> SpriteIndex => _spriteIndex;

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
        
        public static void IncreaseEnteredPlayerCount()
        {
            ++_enteredGoalPlayerCount.Value;
        }
        
        public static void SetTotalPlayerCount(int value)
        {
            _totalPlayerCount.Value = value;
        }

        public static void IncreaseCountDownIndex()
        {
            ++_spriteIndex.Value;
        }

        public static void InitializeCountDown()
        {
            _spriteIndex.Value = 0;
        }
    }
}