using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UniRx;
using UnityEngine;

/// <summary>
/// 맵과 플레이어를 생성하고, 점수를 결산한 뒤 다음 씬을 실행시키는 클래스입니다.
/// </summary>
public class PhotonStageSceneRoomManager : MonoBehaviourPun
{
    #region 스테이지가 시작될 때 수행될 작업.

    private void Awake()
    {
        InitializeMap();
    }

    private void InitializeMap()
    {
        MapData mapData = StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex];

        InstantiateMap(mapData);
        InstantiatePlayer(mapData);
        
        InitializeRx();
    }

    private void InstantiateMap(MapData mapData)
    {
        string filePath = mapData.Data.PrefabFilePath;
        Vector3 mapPos = mapData.Data.MapPosition;
        Quaternion mapRota = mapData.Data.MapRotation;
        
        PhotonNetwork.Instantiate(filePath, mapPos, mapRota);
    }
    
    private void InstantiatePlayer(MapData mapData)
    {
        string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, TagLiteral.Player);
        Vector3 spawnPoint = mapData.Data.PlayerSpawnPosition[PhotonNetwork.LocalPlayer.ActorNumber];
    
        PlayerReferenceManager playerReferenceManager =  
            PhotonNetwork.Instantiate(filePath, spawnPoint, Quaternion.identity).GetComponent<PlayerReferenceManager>();
        
        playerReferenceManager.OnInitialize(this);
    }

    private void InitializeRx()
    {
        StageDataManager.Instance.IsGameActive
            .DistinctUntilChanged()
            .Skip(1)
            .Where(state => !state)
            .Subscribe(_ => CompleteStageAndRankPlayers())
            .AddTo(this);
    }

    #endregion

    #region 스테이지를 결산할 때 수행될 작업.

    // 스테이지를 정리하고 결산하는 역할의 함수입니다.
    // StageDataManager의 IsGameActive가 true => false일 때 호출됩니다.
    private void CompleteStageAndRankPlayers()
    {
        Time.timeScale = 0f;
        
        // 타 컴포넌트에서 게임 결과를 정리하는 로직이 실행되기를 기다립니다.
        PrevEndProduction().Forget();
        
        photonView.RPC("EnterNextScene", RpcTarget.MasterClient);
    }

    private async UniTaskVoid PrevEndProduction()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f), DelayType.UnscaledDeltaTime);
    }

    [PunRPC]
    public void EnterNextScene()
    {
        if (StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex].Info.Type !=
            MapData.MapType.Survivor)
        {
            RankingSettlement();
        }
        else
        {
            GiveScore();
        }
        
        EndLogic();
        
        StageEndProduction().Forget();
    }
    
    private async UniTaskVoid StageEndProduction()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f), DelayType.UnscaledDeltaTime);

        PhotonNetwork.LoadLevel(SceneIndex.GameResult);
    }

    private void GiveScore()
    {
        for (int i = 0; i < StageDataManager.Instance.StagePlayerRankings.Count; i++)
        {
            // 해당 순위의 플레이어가 존재하는지 확인.
            if (i < StageDataManager.Instance.StagePlayerRankings.Count)
            {
                int playerIndex = StageDataManager.Instance.StagePlayerRankings[i];
                if (StageDataManager.Instance.PlayerDataByIndex.ContainsKey(playerIndex))
                {
                    PlayerData playerData = StageDataManager.Instance.PlayerDataByIndex[playerIndex];
                    int oldScore = playerData.Score;
                    int newScore = oldScore + 500;
                    playerData.Score = newScore;
                    StageDataManager.Instance.PlayerDataByIndex[playerIndex] = playerData; // Updated PlayerData back to dictionary
                }
            }
        }
    }

    private void RankingSettlement()
    {
        int[] rankRewards = { 1000, 500, 300 }; // 1등에게 1000점, 2등에게 500점, 3등에게 300점 부여.

        for (int i = 0; i < rankRewards.Length; i++)
        {
            // 해당 순위의 플레이어가 존재하는지 확인.
            if (i < StageDataManager.Instance.StagePlayerRankings.Count)
            {
                int playerIndex = StageDataManager.Instance.StagePlayerRankings[i];
                if (StageDataManager.Instance.PlayerDataByIndex.ContainsKey(playerIndex))
                {
                    PlayerData playerData = StageDataManager.Instance.PlayerDataByIndex[playerIndex];
                    int prevScore = playerData.Score;
                    int updatedScore = prevScore + rankRewards[i];
                    playerData.Score = updatedScore;
                    StageDataManager.Instance.PlayerDataByIndex[playerIndex] = playerData; // Updated PlayerData back to dictionary
                }
            }
        }
    }

    private void EndLogic()
    {
        UpdatePlayerRanking(); 
        StageDataManager.Instance.StagePlayerRankings.Clear();

        // RPC를 호출하여 모든 클라이언트에게 StageDataManager를 업데이트하도록 요청
        photonView.RPC
            ("UpdateStageDataOnAllClients", RpcTarget.All, StageDataManager.Instance.PlayerDataByIndex, StageDataManager.Instance.CachedPlayerIndicesForResults, StageDataManager.Instance.StagePlayerRankings);
    }

    private void UpdatePlayerRanking()
    {
        // PlayerData에 저장된 점수를 기준으로 플레이어를 정렬하고 그 순서대로 인덱스를 CachedPlayerIndicesForResults에 저장
        List<KeyValuePair<int, PlayerData>> sortedPlayers = 
            StageDataManager.Instance.PlayerDataByIndex.OrderByDescending(pair => pair.Value.Score).ToList();

        StageDataManager.Instance.CachedPlayerIndicesForResults.Clear();

        foreach (var pair in sortedPlayers)
        {
            StageDataManager.Instance.CachedPlayerIndicesForResults.Add(pair.Key);
        }
    }
 
    [PunRPC]
    public void UpdateStageDataOnAllClients(Dictionary<int, PlayerData> playerScoresByIndex, List<int> playerRanking, List<int> stagePlayerRankings)
    {
        StageDataManager.Instance.PlayerDataByIndex = playerScoresByIndex;
        StageDataManager.Instance.CachedPlayerIndicesForResults = playerRanking;
        StageDataManager.Instance.StagePlayerRankings = stagePlayerRankings;
    }

    #endregion
}