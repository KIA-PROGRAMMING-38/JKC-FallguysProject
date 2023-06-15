using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Model
{
    public static class RoundResultSceneModel
    {
        // private static readonly ReactiveProperty<bool> _time;
        // public static ReadOnlyReactiveProperty<bool> Time
        //     => _time.ToReadOnlyReactiveProperty();
        
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

        private static readonly ReactiveProperty<int> _firstScore = new IntReactiveProperty(0);
        public static ReadOnlyReactiveProperty<int> FirstScore
            => _firstScore.ToReadOnlyReactiveProperty();
        
        private static readonly ReactiveProperty<int> _secondScore = new IntReactiveProperty(0);
        public static ReadOnlyReactiveProperty<int> SecondScore
            => _secondScore.ToReadOnlyReactiveProperty();

        private static readonly ReactiveProperty<int> _thirdScore = new IntReactiveProperty(0);
        public static ReadOnlyReactiveProperty<int> ThirdScore
            => _thirdScore.ToReadOnlyReactiveProperty();

        /// <summary>
        /// Score를 순차적으로 증가시키는 연출을 하는 함수입니다.
        /// </summary>
        /// <param name="scoreProperty"></param>
        /// <param name="targetScore"></param>
        private static async UniTaskVoid RaiseScore(ReactiveProperty<int> scoreProperty, float targetScore)
        {
            float duration = 0.9f;
            float offset = targetScore / duration;
        
            while (scoreProperty.Value < targetScore)
            {
                scoreProperty.Value += Mathf.RoundToInt(offset * Time.deltaTime);
                await UniTask.Yield();
            }
        
            scoreProperty.Value = (int)targetScore;
        }

        /// <summary>
        /// Score를 순차적으로 증가시키는 연출을 하는 함수를 실행합니다.
        /// </summary>
        public static void PerformScoreRaise()
        {
            RaiseScore(_firstScore, fallGuy[0].Score).Forget();
            RaiseScore(_secondScore, fallGuy[1].Score).Forget();
            RaiseScore(_thirdScore, fallGuy[2].Score).Forget();
        }
    }
}