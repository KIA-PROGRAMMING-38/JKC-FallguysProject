using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class JumpClubDeadZone : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            StageDataManager.Instance.IsPlayerAlive.Value = false;
            StageDataManager.Instance.CurrentState.Value = StageDataManager.PlayerState.Defeat;
            col.gameObject.transform.root.gameObject.SetActive(false);
            
            if (PhotonNetwork.CurrentRoom.PlayerCount >= StageDataManager.Instance.StagePlayerRankings.Count)
            {
                photonView.RPC("RpcEndGameBroadCast", RpcTarget.All);
            }
        }
    }
    
    [PunRPC]
    public void RpcEndGameBroadCast()
    {
        StageDataManager.Instance.SetGameStatus(false);
    }
}
