using LiteralRepository;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PlayerPhotonController playerPhotonController = col.GetComponent<PlayerPhotonController>();
            playerPhotonController.SendMessageWinTheStage();
            
            Debug.Log(playerPhotonController.name);
        }
    }
}
