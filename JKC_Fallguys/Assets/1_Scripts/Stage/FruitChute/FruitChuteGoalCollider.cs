using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class FruitChuteGoalCollider : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PhotonView playerPhotonView = col.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

                StageManager.Instance.PlayerRepository.SetPlayerState(actorNumber, PlayerRepository.PlayerState.Victory);
                StageManager.Instance.PlayerRepository.SetPlayerActive(actorNumber, false);
                
                playerPhotonView.RPC("RpcSetDeActivePlayerObject", RpcTarget.AllBuffered);
        
                photonView.RPC("RpcAddPlayerToRankingOnGoal", RpcTarget.MasterClient, actorNumber);
            }
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
        PlayerRepository playerContainer = StageManager.Instance.PlayerRepository;

        playerContainer.AddPlayerToRanking(playerIndex);

        int rankingCount = playerContainer.StagePlayerRankings.Count;

        if (rankingCount == 3 || PhotonNetwork.CurrentRoom.PlayerCount <= rankingCount)
        {
            photonView.RPC("RpcEndGameBroadCast", RpcTarget.All);
        }
    }


    [PunRPC]
    public void RpcEndGameBroadCast()
    {
        StageManager.Instance.ObjectRepository.SetSequence(ObjectRepository.StageSequence.GameCompletion);
    }
}
