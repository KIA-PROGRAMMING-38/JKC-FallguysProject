using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class JumpClubDeadZone : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PhotonView photonView = col.gameObject.transform.root.GetComponent<PhotonView>();

            if (photonView.IsMine)
            {
                StageDataManager.Instance.IsPlayerAlive.Value = false;
                StageDataManager.Instance.CurrentState.Value = StageDataManager.PlayerState.Defeat;
                col.gameObject.transform.root.gameObject.SetActive(false);
                
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
                Debug.Log(StageDataManager.Instance.StagePlayerRankings.Count);
                if (PhotonNetwork.CurrentRoom.PlayerCount <= StageDataManager.Instance.StagePlayerRankings.Count)
                {
                    photonView.RPC("RpcEndGameBroadCast", RpcTarget.All);
                }
            }
        }
    }
    
    [PunRPC]
    public void RpcEndGameBroadCast()
    {
        StageDataManager.Instance.SetGameStatus(false);
    }
}
