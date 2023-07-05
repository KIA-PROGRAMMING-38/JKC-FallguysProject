using Photon.Pun;

public class PhotonCleaner : MonoBehaviourPun
{
    private void OnDestroy()
    {
        if (photonView.IsMine && gameObject != null)
        {
            photonView.RPC("RpcDestroyOnNetwork", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void RpcDestroyOnNetwork()
    {
        if (photonView == null || photonView.ViewID < 0)
            return;
        
        if (!ReferenceEquals(photonView.Owner, PhotonNetwork.MasterClient))
        {
            photonView.TransferOwnership(PhotonNetwork.MasterClient.ActorNumber);
        }

        PhotonNetwork.Destroy(photonView);
    }
}
