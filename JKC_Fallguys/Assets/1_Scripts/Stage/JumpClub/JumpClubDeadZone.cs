using LiteralRepository;
using UnityEngine;

public class JumpClubDeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PlayerPhotonController playerPhotonController = col.GetComponent<PlayerPhotonController>();
            playerPhotonController.PlayerIsGameOver();

            GameObject rootObject = playerPhotonController.transform.parent.gameObject;
            rootObject.SetActive(false);
        }
    }
}
