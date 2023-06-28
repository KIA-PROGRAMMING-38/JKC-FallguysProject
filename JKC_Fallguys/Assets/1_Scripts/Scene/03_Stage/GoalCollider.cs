using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class GoalCollider : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PlayerPhotonController playerPhotonController = col.GetComponent<PlayerPhotonController>();
            int playerPersonalIndex = PhotonNetwork.LocalPlayer.ActorNumber;
            
            Debug.Log(playerPhotonController.name);
            
            playerPhotonController.photonView.RPC
                ("AddPlayerToRankingOnGoal", RpcTarget.MasterClient, playerPersonalIndex);
        }
    }
}
