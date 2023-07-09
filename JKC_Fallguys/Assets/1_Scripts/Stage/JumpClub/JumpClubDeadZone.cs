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
                StageDataManager.Instance.PlayerContainer.SetPlayerActive(actorNumber, false);
                StageDataManager.Instance.PlayerContainer.SetPlayerState(actorNumber, PlayerContainer.PlayerState.Defeat);
                
                playerPhotonView.RPC("RpcSetDeActivePlayerObject", RpcTarget.AllBuffered);

                photonView.RPC("RpcAddPlayerToFailedList", RpcTarget.All, actorNumber);
            }
        }
    }

    [PunRPC]
    public void RpcAddPlayerToFailedList(int actorNumber)
    {
        PlayerContainer playerContainer = StageDataManager.Instance.PlayerContainer;
    
        playerContainer.AddFailedPlayer(actorNumber);

        int totalPlayerCount = playerContainer.StagePlayerRankings.Count + playerContainer.FailedClearStagePlayers.Count;

        if (PhotonNetwork.CurrentRoom.PlayerCount <= totalPlayerCount)
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
