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
            playerPhotonController.SendMessageWinTheStage();
            int playerPersonalIndex = StageDataManager.Instance.PlayerStageIndex;
            
            Debug.Log(playerPhotonController.name);
            
            playerPhotonController.photonView.RPC("AddPlayerToRankingOnGoal", RpcTarget.MasterClient, playerPersonalIndex);
        }
    }
}
