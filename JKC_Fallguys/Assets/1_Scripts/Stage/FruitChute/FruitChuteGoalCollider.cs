using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class FruitChuteGoalCollider : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            col.gameObject.transform.root.gameObject.SetActive(false);
            StageDataManager.Instance.CurrentState.Value = StageDataManager.PlayerState.Victory;
            
            photonView.RPC("RpcAddPlayerToRankingOnGoal", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }
    
    [PunRPC]
    public void RpcAddPlayerToRankingOnGoal(int playerIndex)
    {
        // 마스터 클라이언트는 모든 클라이언트에게 순위 업데이트를 요청합니다.
        photonView.RPC("RpcUpdatePlayerRanking", RpcTarget.All, playerIndex);
    }

    [PunRPC]
    public void RpcUpdatePlayerRanking(int playerIndex)
    {
        // 각 클라이언트는 이 함수를 통해 자신의 순위 리스트를 최신 상태로 업데이트합니다.
        StageDataManager.Instance.AddPlayerToRanking(playerIndex);

        if (StageDataManager.Instance.StagePlayerRankings.Count == 3 ||
            PhotonNetwork.CurrentRoom.PlayerCount >= StageDataManager.Instance.StagePlayerRankings.Count)
        {
            photonView.RPC("RpcEndGameBroadCast", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RpcEndGameBroadCast()
    {
        StageDataManager.Instance.SetGameStatus(false);
    }
}
