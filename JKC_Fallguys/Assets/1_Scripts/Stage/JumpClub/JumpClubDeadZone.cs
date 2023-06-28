using LiteralRepository;
using UnityEngine;

public class JumpClubDeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            StageDataManager.Instance.IsPlayerAlive.Value = false;
            StageDataManager.Instance.CurrentState.Value = StageDataManager.PlayerState.Defeat;
            
            PlayerPhotonController playerPhotonController = col.GetComponent<PlayerPhotonController>();
            GameObject rootObject = playerPhotonController.transform.parent.gameObject;
            rootObject.SetActive(false);
        }
    }
}
