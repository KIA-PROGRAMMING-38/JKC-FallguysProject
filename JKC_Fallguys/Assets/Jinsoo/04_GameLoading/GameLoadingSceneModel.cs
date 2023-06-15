using UniRx;

namespace Model
{
    public static class GameLoadingSceneModel
    {
        private static ReactiveProperty<bool> _isWhitePanelActive = new ReactiveProperty<bool>(true);
        public static IReadOnlyReactiveProperty<bool> IsWhitePanelActive => _isWhitePanelActive;
        
        // // WhitePanel를 제어하는 값입니다.
        // private static bool _isWhitePanelActive = true;
        // public static bool IsWhitePanelActive
        // {
        //     get { return _isWhitePanelActive; }
        // }
        
        // RandomPick UI들을 제어하는 값입니다.
        // true일 경우 RandomPick UI가 활성화되며, false일 경우 Map Information UI가 활성화됩니다..
        private static bool _isLoadingSceneSwitch = true;
        public static bool IsLoadingSceneSwitch
        {
            get { return _isLoadingSceneSwitch; }
        }
        
        /// <summary>
        /// WhitePanel을 제어할 수 있는 함수입니다. 
        /// </summary>
        /// <param name="status">true일 시 패널 활성화, false일 시 패널 비활성화.</param>
        public static void SetActiveWhitePanel(bool status)
        {
            _isWhitePanelActive.Value = status;
        }

        /// <summary>
        /// LoadingScene의 UI의 활성화를 제어할 수 있는 함수입니다. 
        /// </summary>
        public static void SetStatusLoadingSceneUI(bool status)
        {
            _isLoadingSceneSwitch = status;
        }
    }
}
