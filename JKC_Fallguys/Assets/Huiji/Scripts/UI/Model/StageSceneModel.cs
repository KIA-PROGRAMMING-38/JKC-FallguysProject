namespace Model
{
    public static class StageSceneModel
    {
        private static bool _isExitPanelPopUp;

        public static bool IsExitPanelPopUp => _isExitPanelPopUp;
        
        public static void ActiveExitPanel(bool status)
        {
            _isExitPanelPopUp = status;
        }
    }
}