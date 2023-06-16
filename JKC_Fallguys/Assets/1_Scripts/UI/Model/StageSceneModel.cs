using UniRx;

namespace Model
{
    public static class StageSceneModel
    {
        private static bool _isExitPanelPopUp;
        public static bool IsExitPanelPopUp => _isExitPanelPopUp;

        private static bool _canClickButton;
        public static bool CanClickButton => _canClickButton;
        
        /// <summary>
        /// Exit 패널이 활성화 될 수 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="status"></param>
        public static void SetExitPanelActive(bool status)
        {
            _isExitPanelPopUp = status;
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