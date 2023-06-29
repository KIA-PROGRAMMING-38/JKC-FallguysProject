using UniRx;

namespace Model
{
    public static class ResultSceneModel
    {
        private static readonly ReactiveProperty<bool> _isVictorious = new ReactiveProperty<bool>();
        public static ReactiveProperty<bool> IsVictorious => _isVictorious;

        public static void CheckVictory(bool status)
        {
            _isVictorious.Value = status;
        }
    }
}