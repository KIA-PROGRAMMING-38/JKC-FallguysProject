using Photon.Pun;

public class PhotonCleaner : MonoBehaviourPun
{
    private void Awake()
    {
        StageRepository.Instance.OnPlayerDispose -= TransferPhotonViewOwnership;
        StageRepository.Instance.OnPlayerDispose += TransferPhotonViewOwnership;
    }

    private void TransferPhotonViewOwnership()
    {
        if (!photonView.IsMine)
            return;
        
        photonView.TransferOwnership(PhotonNetwork.MasterClient.ActorNumber);
    }

    private void OnDestroy()
    {
        if (photonView.IsMine && gameObject != null)
        {
            photonView.RPC("RpcDestroyOnNetwork", RpcTarget.MasterClient);
        }
        
        StageRepository.Instance.OnPlayerDispose -= TransferPhotonViewOwnership;
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
