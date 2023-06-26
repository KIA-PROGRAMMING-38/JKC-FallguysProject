using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class PhotonStageSceneRoomManager : MonoBehaviourPun
{
    /// <summary>
    /// 스테이지를 정리하고 결산하는 역할의 함수입니다.
    /// </summary>
    [PunRPC]
    public void CompleteStageAndRankPlayers()
    {
        // 승리한 클라이언트의 정보에만 승리 기록해주기.
        // 나머지는 자동으로 패배가 기록되어, 다음 결과 창에서 승리 혹은 패배가 팝업됨.
        Debug.Log("승리!");
        Time.timeScale = 0f;
        
        photonView.RPC("EnterNextScene", RpcTarget.MasterClient);
    }

    private async UniTaskVoid StageEndProduction()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f), DelayType.UnscaledDeltaTime);

        PhotonNetwork.LoadLevel(SceneIndex.GameResult);
    }

    [PunRPC]
    public void EnterNextScene()
    {
        RankingSettlement();
        
        StageEndProduction().Forget();
    }

    private void RankingSettlement()
    {
        int[] rewards = { 1000, 500, 300 }; // 1등에게 1000점, 2등에게 500점, 3등에게 300점 부여.

        for (int i = 0; i < rewards.Length; i++)
        {
            // 해당 순위의 플레이어가 존재하는지 확인.
            if (i < StageDataManager.Instance.StagePlayerRankings.Count)
            {
                int playerIndex = StageDataManager.Instance.StagePlayerRankings[i];
                if (StageDataManager.Instance.PlayerScoresByIndex.ContainsKey(playerIndex))
                {
                    PlayerData playerData = StageDataManager.Instance.PlayerScoresByIndex[playerIndex];
                    int oldScore = playerData.Score;
                    int newScore = oldScore + rewards[i];
                    playerData.Score = newScore;
                    StageDataManager.Instance.PlayerScoresByIndex[playerIndex] = playerData; // Updated PlayerData back to dictionary
                }
            }
        }

        UpdatePlayerRanking(); 
        StageDataManager.Instance.StagePlayerRankings.Clear();

        // RPC를 호출하여 모든 클라이언트에게 StageDataManager를 업데이트하도록 요청
        photonView.RPC
            ("UpdateStageDataOnAllClients", RpcTarget.All, StageDataManager.Instance.PlayerScoresByIndex, StageDataManager.Instance.CachedPlayerIndicesForResults, StageDataManager.Instance.StagePlayerRankings);
    }

    private void UpdatePlayerRanking()
    {
        // PlayerData에 저장된 점수를 기준으로 플레이어를 정렬하고 그 순서대로 인덱스를 CachedPlayerIndicesForResults에 저장
        var sortedPlayers = 
            StageDataManager.Instance.PlayerScoresByIndex.OrderByDescending(pair => pair.Value.Score).ToList();

        StageDataManager.Instance.CachedPlayerIndicesForResults.Clear();

        foreach (var pair in sortedPlayers)
        {
            StageDataManager.Instance.CachedPlayerIndicesForResults.Add(pair.Key);
        }
    }

 
    [PunRPC]
    public void UpdateStageDataOnAllClients(Dictionary<int, PlayerData> playerScoresByIndex, List<int> playerRanking, List<int> stagePlayerRankings)
    {
        StageDataManager.Instance.PlayerScoresByIndex = playerScoresByIndex;
        StageDataManager.Instance.CachedPlayerIndicesForResults = playerRanking;
        StageDataManager.Instance.StagePlayerRankings = stagePlayerRankings;
    }
}
