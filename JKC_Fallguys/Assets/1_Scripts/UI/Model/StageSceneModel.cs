using UniRx;

namespace Model
{
    public static class StageSceneModel
    {
        // 패널의 활성화 여부를 결정하는 불리언 값입니다.
        private static readonly ReactiveProperty<bool> _isExitPanelPopUp = new ReactiveProperty<bool>(false);
        public static ReadOnlyReactiveProperty<bool> IsExitPanelPopUp => _isExitPanelPopUp.ToReadOnlyReactiveProperty();
        
        // 포톤 룸에 입장했음을 알리는 불리언 값입니다.
        private static ReactiveProperty<bool> _isEnterPhotonRoom = new ReactiveProperty<bool>(true);
        public static ReadOnlyReactiveProperty<bool> IsEnterPhotonRoom =>
            _isEnterPhotonRoom.ToReadOnlyReactiveProperty();

        // Exit Button을 누를 수 있는지를 결정하는 불리언 값입니다.
        private static bool _canClickButton;
        public static bool CanClickButton => _canClickButton;
        
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
            _canClickButton = status;
        }
        
        private static readonly ReactiveProperty<int> _enteredGoalPlayerCount = new IntReactiveProperty(0);
        
        /// <summary>
        /// 현재 포톤 룸에 입장했음을 알려줄 수 있는 함수입니다.
        /// </summary>
        /// <param name="status">입장했을 경우 true, 나갈 경우 false를 인수로 넘겨줍니다.</param>
        public static void RoomAdmissionStatus(bool status)
        {
            _isEnterPhotonRoom.Value = status;
        }
        
        /// <summary>
        /// 현재 골에 들어간 플레이어의 수를 나타냅니다.
        /// </summary>
        public static ReadOnlyReactiveProperty<int> EnteredGoalPlayerCount
            => _enteredGoalPlayerCount.ToReadOnlyReactiveProperty();

        public static void IncreaseEnteredPlayerCount()
        {
            ++_enteredGoalPlayerCount.Value;
        }
        
        private static readonly ReactiveProperty<int> _totalPlayerCount = new IntReactiveProperty(0);
        
        /// <summary>
        /// Stage에 입장한 플레이어의 수를 나타냅니다.
        /// </summary>
        public static ReadOnlyReactiveProperty<int> TotalPlayerCount
            => _totalPlayerCount.ToReadOnlyReactiveProperty();

        public static void SetTotalPlayerCount(int value)
        {
            _totalPlayerCount.Value = value;
        }
    }
}