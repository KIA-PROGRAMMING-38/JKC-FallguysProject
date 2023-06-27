using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Model
{
    public static class RoundResultSceneModel
    {
        // Stage에 입장했던 플레이어들을 List에 저장합니다.
        private static List<PlayerData> _fallguyRankings = new List<PlayerData>();
        public static List<PlayerData> FallguyRankings => _fallguyRankings;

        static RoundResultSceneModel()
        {
            AddStageResultToList();
        }

        private static void AddStageResultToList()
        {
            // CachedPlayerIndicesForResults 리스트의 모든 요소에 대해 반복합니다.
            foreach (int index in StageDataManager.Instance.CachedPlayerIndicesForResults)
            {
                // 딕셔너리에서 해당 인덱스의 플레이어 데이터를 가져옵니다.
                if (StageDataManager.Instance.PlayerScoresByIndex.TryGetValue(index, out PlayerData playerData))
                {
                    _fallguyRankings.Add(playerData);
                }
                else
                {
                    Debug.Log("Rankings List에 추가 안됨.");
                }
            }    
        }

        private static readonly ReactiveProperty<int> _firstScore = new IntReactiveProperty(0);
        public static IReadOnlyReactiveProperty<int> FirstScore => _firstScore;
        
        private static readonly ReactiveProperty<int> _secondScore = new IntReactiveProperty(0);
        public static IReadOnlyReactiveProperty<int> SecondScore => _secondScore;

        private static readonly ReactiveProperty<int> _thirdScore = new IntReactiveProperty(0);
        public static IReadOnlyReactiveProperty<int> ThirdScore => _thirdScore;

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
            RaiseScore(_firstScore, _fallguyRankings[0].Score).Forget();
            RaiseScore(_secondScore, _fallguyRankings[1].Score).Forget();
            RaiseScore(_thirdScore, _fallguyRankings[2].Score).Forget();
        }
    }
}