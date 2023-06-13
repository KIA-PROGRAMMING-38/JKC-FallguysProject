using Photon.Pun;
using UnityEngine;

public class PlayerPhotonController : MonoBehaviour
{
    private PlayerReferenceManager _referenceManager;

    [PunRPC]
    public void SendMessageWinTheStage()
    {
        _referenceManager.PhotonStageSceneRoomManager.WinTheStage();
    }
    
    public void OnInitialize(PlayerReferenceManager referenceManager)
    {
        _referenceManager = referenceManager;
    }
}
