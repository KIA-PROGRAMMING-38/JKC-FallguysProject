using UniRx;

namespace Model
{
    public static class ResultSceneModel
    {
        private static readonly ReactiveProperty<bool> _isVictorious = new ReactiveProperty<bool>();

        public static ReactiveProperty<bool> IsVictorious
        {
            get
            {
                return _isVictorious;
            }

            set
            {
                _isVictorious.Value = value.Value;
            }
        }
    }
}