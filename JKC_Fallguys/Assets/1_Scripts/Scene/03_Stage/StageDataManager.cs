using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class StageDataManager : SingletonMonoBehaviour<StageDataManager>
{
    public int PlayerStageIndex;
    
    // 플레이어의 점수들이 계속해서 저장되는 딕셔너리입니다.
    public Dictionary<int, int> PlayerScoresByIndex;
    // 스테이지에서 사용될 순위를 기록하는 리스트입니다.
    // 스테이지가 넘어갈 때, 초기화됩니다.
    public List<int> StagePlayerRankings;
    
    protected override void Awake()
    {
        base.Awake();

        GetPlayerIndex();
    }
    
    public void AddPlayerToRanking(int playerIndex)
    {
        StagePlayerRankings.Add(playerIndex);

        foreach (var elem in StagePlayerRankings)
        {
            Debug.Log($"In PlayerIndexList, CurrentPlayer : {elem}");
        }
    }

    private void GetPlayerIndex()
    {
        Photon.Realtime.Player localPlayer = PhotonNetwork.LocalPlayer;
        Hashtable playerProperties = localPlayer.CustomProperties;

        if (playerProperties.TryGetValue("PersonalIndex", out object personalIndexObj))
        {
            PlayerStageIndex = (int)personalIndexObj;
        }
    }

    /// <summary>
    /// StateDataManager는 Singleton으로 구성되어 있습니다.
    /// 이 클래스는, Loading이 시작될 때 생성되고 Lobby로 돌아갈 경우 파괴되어야 합니다.
    /// 이를 위한 public 함수입니다.
    /// </summary>
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}
