namespace Model
{
    public static class GameLoadingSceneModel
    {
        // WhitePanel의 활성화와 비활성화를 담당하는 값.
        private static bool _isWhitePanelActive;
        public static bool IsWhitePanelActive
        {
            get { return _isWhitePanelActive; }
        }

        /// <summary>
        /// WhitePanel을 제어할 수 있는 함수 
        /// </summary>
        /// <param name="status">true일 시 패널 활성화, false일 시 패널 비활성화.</param>
        public static void SetActiveWhitePanel(bool status)
        {
            _isWhitePanelActive = status;
        }
    }
}
