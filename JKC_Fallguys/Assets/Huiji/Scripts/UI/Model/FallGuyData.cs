namespace Model
{
    public class FallGuyData
    {
        private int _score;
        public int Score => _score;

        public FallGuyData(int score)
        {
            _score = score;
        }
    }
}