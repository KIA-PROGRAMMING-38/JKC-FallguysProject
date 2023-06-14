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
    }
}