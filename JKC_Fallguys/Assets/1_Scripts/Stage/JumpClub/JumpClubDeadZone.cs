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
                StageManager.Instance.PlayerRepository.SetPlayerActive(actorNumber, false);
                StageManager.Instance.PlayerRepository.SetPlayerState(actorNumber, PlayerRepository.PlayerState.Defeat);
                
                playerPhotonView.RPC("RpcSetDeActivePlayerObject", RpcTarget.AllBuffered);

                photonView.RPC("RpcAddPlayerToFailedList", RpcTarget.All, actorNumber);
            }
        }
    }

    [PunRPC]
    public void RpcAddPlayerToFailedList(int actorNumber)
    {
        PlayerRepository playerContainer = StageManager.Instance.PlayerRepository;
    
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
        StageManager.Instance.ObjectRepository.SetSequence(ObjectRepository.StageSequence.GameCompletion);
        
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if (!StageManager.Instance.PlayerRepository.IsPlayerActive(actorNumber).Value)
        {
            StageManager.Instance.PlayerRepository.SetPlayerState(actorNumber, PlayerRepository.PlayerState.GameTerminated);
        }
    }
}
