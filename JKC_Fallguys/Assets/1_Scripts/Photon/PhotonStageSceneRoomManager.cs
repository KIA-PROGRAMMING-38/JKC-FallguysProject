using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Model;
using Newtonsoft.Json;
using Photon.Pun;
using UniRx;
using UnityEngine;

/// <summary>
/// 점수를 결산한 뒤 다음 씬을 실행시키는 클래스입니다.
/// </summary>
public class PhotonStageSceneRoomManager : MonoBehaviourPun
{
    #region 스테이지가 시작될 때 수행될 작업.

    private int _readyClientsCount = 0;
    private double _serverTime;
    private const int GameStartDelaySeconds = 2;
    private const int OperationCountdownDelaySeconds = 3;

    private CancellationTokenSource _cts = new CancellationTokenSource();

    private void Awake()
    {
        InitializeRx();
        StageDontDestroyOnLoadSet();

        _cts = new CancellationTokenSource();
        Debug.Log($"MasterClient ActNum: {PhotonNetwork.MasterClient.ActorNumber}");

        if (PhotonNetwork.IsMasterClient)
        {
            StageDataManager.Instance.PhotonTimeHelper.SyncServerTime(_cts.Token).Forget();
            WaitForAllClientsLoaded().Forget();
        }
    
        photonView.RPC("IsClientReady", RpcTarget.MasterClient);
    }


    private async UniTask WaitForAllClientsLoaded()
    {
        while (_readyClientsCount < PhotonNetwork.CountOfPlayersOnMaster)
        {
            await UniTask.Delay(TimeSpan.FromMilliseconds(PhotonTimeHelper.SyncIntervalMs), cancellationToken: _cts.Token);
        }

        ScheduleGameStart
            (StageDataManager.Instance.PhotonTimeHelper.GetFutureNetworkTime(GameStartDelaySeconds));
        ScheduleOperationCountdown
            (StageDataManager.Instance.PhotonTimeHelper.GetFutureNetworkTime(OperationCountdownDelaySeconds));
    }

    private void ScheduleGameStart(double startTime)
    {
        Debug.Log("ScheduleGameStart");
        photonView.RPC("RpcSetGameStart", RpcTarget.All, startTime);
    }

    private void ScheduleOperationCountdown(double startTime)
    {
        Debug.Log("ScheduleOperationCountdown");
        photonView.RPC("RpcStartTriggerOperationCountDown", RpcTarget.All, startTime);
    }

    [PunRPC]
    public void IsClientReady()
    {
        ++_readyClientsCount;
    }

    [PunRPC]
    public void RpcSetGameStart(double startPhotonNetworkTime)
    {
        StageDataManager.Instance.PhotonTimeHelper.ScheduleDelayedAction(startPhotonNetworkTime, SetGameStart, _cts.Token);
    }

    [PunRPC]
    public void RpcStartTriggerOperationCountDown(double startPhotonNetworkTime)
    {
        StageDataManager.Instance.PhotonTimeHelper.ScheduleDelayedAction(startPhotonNetworkTime, StartOperationCountdown, _cts.Token);
    }

    private void SetGameStart()
    {
        StageDataManager.Instance.SetGameStart(true);
    }

    private void StartOperationCountdown()
    {
        Observable.Interval(TimeSpan.FromSeconds(1.5))
            .TakeWhile(_ => StageSceneModel.SpriteIndex.Value <= 4)
            .Subscribe(_ => StageSceneModel.IncreaseCountDownIndex())
            .AddTo(this);
    }

    private void OnDestroy()
    {
        _cts.Cancel();
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


    private void StageDontDestroyOnLoadSet()
    {
        DontDestroyOnLoad(gameObject);
        photonView.RPC("RpcSetParentStageRepository", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcSetParentStageRepository()
    {
        transform.SetParent(StageRepository.Instance.gameObject.transform);
    }

    #endregion

    #region 스테이지를 결산할 때 수행될 작업.

    // 스테이지를 정리하고 결산하는 역할의 함수입니다.
    // StageDataManager의 IsGameActive가 true => false일 때 호출됩니다.
    private void CompleteStageAndRankPlayers()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        // 타 컴포넌트에서 게임 결과를 정리하는 로직이 실행되기를 기다립니다.
        PrevEndProduction().Forget();
    }

    private async UniTaskVoid PrevEndProduction()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(4f), DelayType.UnscaledDeltaTime);

        photonView.RPC("SetRoundState", RpcTarget.All);
        EnterNextScene();
    }

    [PunRPC]
    public void SetRoundState()
    {
        StageDataManager.Instance.SetRoundState(true);
    }

    private void EnterNextScene()
    {
        if (StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex.Value].Info.Type !=
            MapData.MapType.Survivor)
        {
            RankingSettlement();
        }
        else
        {
            GiveScore();
        }

        CalculateLosersScore();
        EndLogic();

        photonView.RPC("RpcEveryClientPhotonViewTransferOwnerShip", RpcTarget.AllBuffered);

        StageEndProduction().Forget();
    }

    [PunRPC]
    public void RpcEveryClientPhotonViewTransferOwnerShip()
    {
        StageRepository.Instance.PlayerDispose();
    }


    private async UniTaskVoid StageEndProduction()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5f), DelayType.UnscaledDeltaTime);

        photonView.RPC("RpcClearPlayerObject", RpcTarget.MasterClient);

        if (StageDataManager.Instance.IsFinalRound())
        {
            PhotonNetwork.LoadLevel(SceneIndex.GameResult);
        }
        else if (!StageDataManager.Instance.IsFinalRound())
        {
            PhotonNetwork.LoadLevel(SceneIndex.RoundResult);
        }
    }

    [PunRPC]
    public void RpcClearPlayerObject()
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in StageDataManager.Instance.gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        foreach (Transform child in StageRepository.Instance.gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        foreach (GameObject child in children)
        {
            PhotonView childPhotonView = child.GetComponent<PhotonView>();

            if (childPhotonView == null || childPhotonView.ViewID < 0)
                continue;

            PhotonNetwork.Destroy(childPhotonView);
        }
    }

    private void GiveScore()
    {
        for (int i = 0; i < StageDataManager.Instance.PlayerContainer.StagePlayerRankings.Count; i++)
        {
            // 해당 순위의 플레이어가 존재하는지 확인.
            if (i < StageDataManager.Instance.PlayerContainer.StagePlayerRankings.Count)
            {
                int playerIndex = StageDataManager.Instance.PlayerContainer.StagePlayerRankings[i];
                if (StageDataManager.Instance.PlayerContainer.PlayerDataByIndex.ContainsKey(playerIndex))
                {
                    PlayerData playerData = StageDataManager.Instance.PlayerContainer.PlayerDataByIndex[playerIndex];
                    int oldScore = playerData.Score;
                    int newScore = oldScore + 2500;
                    playerData.Score = newScore;
                    StageDataManager.Instance.PlayerContainer.PlayerDataByIndex[playerIndex] =
                        playerData; // Updated PlayerData back to dictionary
                }
            }
        }
    }

    private void RankingSettlement()
    {
        int[] rankRewards = { 5000, 2000, 500 };

        for (int i = 0; i < rankRewards.Length; i++)
        {
            // 해당 순위의 플레이어가 존재하는지 확인.
            if (i < StageDataManager.Instance.PlayerContainer.StagePlayerRankings.Count)
            {
                int playerIndex = StageDataManager.Instance.PlayerContainer.StagePlayerRankings[i];
                if (StageDataManager.Instance.PlayerContainer.PlayerDataByIndex.ContainsKey(playerIndex))
                {
                    PlayerData playerData = StageDataManager.Instance.PlayerContainer.PlayerDataByIndex[playerIndex];
                    int prevScore = playerData.Score;
                    int updatedScore = prevScore + rankRewards[i];
                    playerData.Score = updatedScore;
                    StageDataManager.Instance.PlayerContainer.PlayerDataByIndex[playerIndex] =
                        playerData; // Updated PlayerData back to dictionary
                }
            }
        }
    }

    private void CalculateLosersScore()
    {
        foreach (int elem in StageDataManager.Instance.PlayerContainer.FailedClearStagePlayers)
        {
            if (StageDataManager.Instance.PlayerContainer.PlayerDataByIndex.ContainsKey(elem))
            {
                PlayerData playerData = StageDataManager.Instance.PlayerContainer.PlayerDataByIndex[elem];
                int prevScore = playerData.Score;
                int updatedScore = prevScore + 100;
                playerData.Score = updatedScore;
                StageDataManager.Instance.PlayerContainer.PlayerDataByIndex[elem] = playerData;
            }
        }
    }

    private void EndLogic()
    {
        UpdatePlayerRanking();
        StageDataManager.Instance.PlayerContainer.StagePlayerRankings.Clear();
        StageDataManager.Instance.PlayerContainer.FailedClearStagePlayers.Clear();

        string playerScoresByIndexJson =
            JsonConvert.SerializeObject(StageDataManager.Instance.PlayerContainer.PlayerDataByIndex);

        photonView.RPC("UpdateStageDataOnAllClients", RpcTarget.All, playerScoresByIndexJson,
            StageDataManager.Instance.PlayerContainer.CachedPlayerIndicesForResults.ToArray(),
            StageDataManager.Instance.PlayerContainer.StagePlayerRankings.ToArray());
    }


    private void UpdatePlayerRanking()
    {
        // PlayerData에 저장된 점수를 기준으로 플레이어를 정렬하고 그 순서대로 인덱스를 CachedPlayerIndicesForResults에 저장합니다.
        List<KeyValuePair<int, PlayerData>> sortedPlayers =
            StageDataManager.Instance.PlayerContainer.PlayerDataByIndex.OrderByDescending(pair => pair.Value.Score)
                .ToList();

        StageDataManager.Instance.PlayerContainer.CachedPlayerIndicesForResults.Clear();

        foreach (KeyValuePair<int, PlayerData> pair in sortedPlayers)
        {
            StageDataManager.Instance.PlayerContainer.CachedPlayerIndicesForResults.Add(pair.Key);
        }
    }

    [PunRPC]
    public void UpdateStageDataOnAllClients(string playerScoresByIndexJson, int[] playerRanking,
        int[] stagePlayerRankings)
    {
        StageDataManager.Instance.PlayerContainer.PlayerDataByIndex =
            JsonConvert.DeserializeObject<Dictionary<int, PlayerData>>(playerScoresByIndexJson);
        StageDataManager.Instance.PlayerContainer.CachedPlayerIndicesForResults = playerRanking.ToList();
        StageDataManager.Instance.PlayerContainer.StagePlayerRankings = stagePlayerRankings.ToList();
    }

    #endregion
}
