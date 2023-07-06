using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class JumpClubDeadZone : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PhotonView playerPhotonView = col.GetComponent<PhotonView>();

            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                playerPhotonView.transform.parent.gameObject.SetActive(false);
                
                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                StageDataManager.Instance.SetPlayerActive(actorNumber, false);
                StageDataManager.Instance.SetPlayerState(actorNumber, StageDataManager.PlayerState.Defeat);
                
                playerPhotonView.RPC("RpcSetDeActivePlayerObject", RpcTarget.AllBuffered);

                photonView.RPC("RpcAddPlayerToFailedList", RpcTarget.All, actorNumber);
            }
        }
    }

    [PunRPC]
    public void RpcAddPlayerToFailedList(int actorNumber)
    {
        StageDataManager.Instance.AddPlayerToFailedClearStagePlayers(actorNumber);

        if (PhotonNetwork.CurrentRoom.PlayerCount <= 
            StageDataManager.Instance.StagePlayerRankings.Count + StageDataManager.Instance.FailedClearStagePlayers.Count)
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
