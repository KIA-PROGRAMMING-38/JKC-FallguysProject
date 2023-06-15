using System.Collections.Generic;

namespace Model
{
    public static class RoundResultSceneModel
    {
        // Stage에 입장했던 플레이어들을 List에 저장합니다.
        private static List<FallGuyData> fallGuy = new List<FallGuyData>()
        {
            new FallGuyData(100),
            new FallGuyData(200),
            new FallGuyData(300),
            new FallGuyData(700),
            new FallGuyData(1000)
        };

        static RoundResultSceneModel()
        {
            SortFallGuysByScore();
            SetRankingScores();
        }
        
        /// <summary>
        /// Score 순으로 내림차순 정렬합니다.
        /// </summary>
        private static void SortFallGuysByScore()
        {
            fallGuy.Sort((x, y) =>
            {
                if (x.Score == y.Score)
                {
                    return 0;
                }
                
                else if (x.Score > y.Score)
                {
                    return -1;
                }

                else
                {
                    return 1;
                }
            });
        }

        private static int _firstScore;
        public static int FirstScore => _firstScore;
        
        private static int _secondScore;
        public static int SecondScore => _secondScore;
        
        private static int _thirdScore;
        public static int ThirdScore => _thirdScore;

        private static void SetRankingScores()
        {
            _firstScore = fallGuy[0].Score;
            _secondScore = fallGuy[1].Score;
            _thirdScore = fallGuy[2].Score;
        }
    }
}