namespace Model
{
    public static class LoginSceneModel
    {
        // LoginPanel의 InputField가 올바르게 입력되었는지 검사합니다.
        private static bool _isCorrectInput = false;

        public static bool IsLoginInputFieldFilled
        {
            get { return _isCorrectInput; }
        }

        public static void ConditionEstablished()
        {
            _isCorrectInput = true;
        }

        public static void NotConditionEstablished()
        {
            _isCorrectInput = false;
        }
    }
}