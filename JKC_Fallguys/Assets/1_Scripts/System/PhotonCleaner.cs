using Photon.Pun;

public class PhotonCleaner : MonoBehaviourPun
{
    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("DestroyOnNetwork", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void DestroyOnNetwork()
    {
        if (photonView.Owner != PhotonNetwork.MasterClient)
        {
            photonView.TransferOwnership(PhotonNetwork.MasterClient.ActorNumber);
        }

        PhotonNetwork.Destroy(photonView);
    }
}
