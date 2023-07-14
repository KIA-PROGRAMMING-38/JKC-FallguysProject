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
            if (StageManager.Instance.PlayerRepository.PlayerDataByIndex.ContainsKey(playerIndex))
            {
                PlayerData playerData = StageManager.Instance.PlayerRepository.PlayerDataByIndex[playerIndex];
                int previousScore = playerData.Score;
                int updatedScore = previousScore + scoreToAdd;
                playerData.Score = updatedScore;
                StageManager.Instance.PlayerRepository.PlayerDataByIndex[playerIndex] = playerData;
            }
        }

        private static void GiveScore()
        {
            for (int i = 0; i < StageManager.Instance.PlayerRepository.StagePlayerRankings.Count; i++)
            {
                int playerIndex = StageManager.Instance.PlayerRepository.StagePlayerRankings[i];
                UpdatePlayerScore(playerIndex, 2500);
            }
        }

        private static void RankingSettlement()
        {
            int[] rankRewards = { 5000, 2000, 500 };

            for (int i = 0; i < rankRewards.Length; i++)
            {
                if (i < StageManager.Instance.PlayerRepository.StagePlayerRankings.Count)
                {
                    int playerIndex = StageManager.Instance.PlayerRepository.StagePlayerRankings[i];
                    UpdatePlayerScore(playerIndex, rankRewards[i]);
                }
            }
        }

        private static void CalculateLosersScore()
        {
            foreach (int playerIndex in StageManager.Instance.PlayerRepository.FailedClearStagePlayers)
            {
                UpdatePlayerScore(playerIndex, 100);
            }
        }
    }
}
