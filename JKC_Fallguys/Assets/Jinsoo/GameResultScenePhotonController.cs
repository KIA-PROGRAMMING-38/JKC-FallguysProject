using Photon.Pun;

public class GameResultScenePhotonController : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
