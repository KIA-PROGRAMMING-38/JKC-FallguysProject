using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class JumpClubDeadZone : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            if (photonView.IsMine)
            {
                StageDataManager.Instance.IsPlayerAlive.Value = false;
                StageDataManager.Instance.CurrentState.Value = StageDataManager.PlayerState.Defeat;
                col.gameObject.transform.root.gameObject.SetActive(false);

                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                photonView.RPC("RpcAddPlayerToFailedList", RpcTarget.MasterClient, actorNumber);
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
