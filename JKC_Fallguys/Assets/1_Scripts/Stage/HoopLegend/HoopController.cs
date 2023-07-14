using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiteralRepository;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UnityEngine;

public class HoopController : MonoBehaviourPun
{
    private Dictionary<int, int> _playerHoopCounts = new Dictionary<int, int>();
    private HoopLegendController _hoopLegendController;
    
    private void Awake()
    {
        _hoopLegendController = GetComponentInParent<HoopLegendController>();
        Debug.Assert(_hoopLegendController != null);
        
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            _playerHoopCounts[player.ActorNumber] = 0;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            InitializeObject();
            InitializeRx();
        }
    }

    private void InitializeObject()
    {
        ObjectTransforms commonHoopData = ResourceManager.JsonLoader<ObjectTransforms>($"Data/CommonHoopTransformData");
        ObjectTransforms specialHoopData = ResourceManager.JsonLoader<ObjectTransforms>($"Data/SpecialHoopTransformData");

        for (int i = 0; i < commonHoopData.positions.Length; ++i)
        {
            string filePath = Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.Stage, PathLiteral.HoopLegend, "CommonHoop");
            
            PhotonNetwork.Instantiate(filePath, commonHoopData.positions[i], Quaternion.Euler(commonHoopData.rotations[i]));
        }
        
        for (int i = 0; i < specialHoopData.positions.Length; ++i)
        {
            string filePath = Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.Stage, PathLiteral.HoopLegend, "SpecialHoop");
            
            PhotonNetwork.Instantiate(filePath, specialHoopData.positions[i], Quaternion.Euler(specialHoopData.rotations[i]));
        }
    }

    private void InitializeRx()
    {
        // 게임이 비활성화 되면, 후프 카운트를 기반으로 플레이어 순위를 계산합니다.
        StageManager.Instance.ObjectRepository.CurrentSequence
            .Where(sequence => sequence == ObjectRepository.StageSequence.GameCompletion && PhotonNetwork.IsMasterClient)
            .Subscribe(_ => CalculatePlayerRanking())
            .AddTo(this);
    }

    // 마스터 클라이언트에서만 호출됩니다.
    private void CalculatePlayerRanking()
    {
        // 후프 카운트가 높은 플레이어부터 정렬하여 ActorNumber를 리스트로 변환합니다.
        // 이 리스트는 플레이어의 순위를 나타냅니다.
        List<int> rankings = _playerHoopCounts
            .OrderByDescending(x => x.Value)
            .Select(x => x.Key)
            .ToList();
        
        photonView.RPC("UpdatePlayerRankings", RpcTarget.All, rankings.ToArray());
    }

    [PunRPC]
    public void UpdatePlayerRankings(int[] rankings)
    {
        for (int i = 0; i < rankings.Length; i++)
        {
            if (i < 3) 
            {
                StageManager.Instance.PlayerRepository.AddPlayerToRanking(rankings[i]);
            }
            else
            {
                StageManager.Instance.PlayerRepository.AddFailedPlayer(rankings[i]);
            }
        }
    }



    /// <summary>
    /// 마스터 클라이언트에서만 후프 카운트를 증가시키고, 새로운 카운트를 모든 클라이언트에게 전송합니다.
    /// </summary>
    [PunRPC]
    public void IncreaseCountAndBroadcast(int actorNumber, int increaseValue)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 모든 클라이언트의 플레이어 후프 카운트를 갱신합니다.
            photonView.RPC("UpdateHoopCount", RpcTarget.All, actorNumber, increaseValue);
        }
    }

    /// <summary>
    /// 마스터 클라이언트가 후프 카운트를 변경하면 호출됩니다.
    /// 변경된 카운트를 자신의 로컬 _playerHoopCounts Dictionary에 업데이트합니다.
    /// </summary>
    [PunRPC]
    public void UpdateHoopCount(int actorNumber, int increaseValue)
    {
        // 플레이어의 후프 카운트가 아직 Dictionary에 없다면, 먼저 0으로 초기화합니다.
        _playerHoopCounts.TryAdd(actorNumber, 0);

        // 해당 플레이어의 키에 대응하여 값을 증가시킵니다.
        _playerHoopCounts[actorNumber] += increaseValue;
    }

    /// <summary>
    /// 플레이어가 후프를 지나가면 호출됩니다.
    /// 자신의 ActorNumber와 Value를 HoopController.IncreaseCountAndBroadcast 메서드에 전달합니다.
    /// </summary> 
    /// <param name="value">플레이어가 획득한 점수입니다..</param>
    public void PlayerPassesHoop(int value)
    {
        photonView.RPC("IncreaseCountAndBroadcast", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber, value);
    }
}
