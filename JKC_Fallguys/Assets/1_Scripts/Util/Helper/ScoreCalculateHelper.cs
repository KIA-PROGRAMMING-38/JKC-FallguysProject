namespace Util.Helper
{
    public static class ScoreCalculateHelper
    {
        public static void Calculate()
        {
            if (StageManager.Instance.ObjectRepository.MapDatas[StageManager.Instance.ObjectRepository.MapPickupIndex.Value].Info.Type !=
                MapData.MapType.Survivor)
            {
                RankingSettlement();
            }
            else
            {
                GiveScore();
            }

            CalculateLosersScore();
        }

        private static void UpdatePlayerScore(int playerIndex, int scoreToAdd)
        {
            if (StageManager.Instance.PlayerContainer.PlayerDataByIndex.ContainsKey(playerIndex))
            {
                PlayerData playerData = StageManager.Instance.PlayerContainer.PlayerDataByIndex[playerIndex];
                int previousScore = playerData.Score;
                int updatedScore = previousScore + scoreToAdd;
                playerData.Score = updatedScore;
                StageManager.Instance.PlayerContainer.PlayerDataByIndex[playerIndex] = playerData;
            }
        }

        private static void GiveScore()
        {
            for (int i = 0; i < StageManager.Instance.PlayerContainer.StagePlayerRankings.Count; i++)
            {
                int playerIndex = StageManager.Instance.PlayerContainer.StagePlayerRankings[i];
                UpdatePlayerScore(playerIndex, 2500);
            }
        }

        private static void RankingSettlement()
        {
            int[] rankRewards = { 5000, 2000, 500 };

            for (int i = 0; i < rankRewards.Length; i++)
            {
                if (i < StageManager.Instance.PlayerContainer.StagePlayerRankings.Count)
                {
                    int playerIndex = StageManager.Instance.PlayerContainer.StagePlayerRankings[i];
                    UpdatePlayerScore(playerIndex, rankRewards[i]);
                }
            }
        }

        private static void CalculateLosersScore()
        {
            foreach (int playerIndex in StageManager.Instance.PlayerContainer.FailedClearStagePlayers)
            {
                UpdatePlayerScore(playerIndex, 100);
            }
        }
    }
}
