using UnityEngine;
using Photon.Pun;

public class PhotonClear : MonoBehaviour
{
    private void OnDestroy()
    {
        foreach (PhotonView photonView in PhotonNetwork.PhotonViewCollection)
        {
            if (photonView.IsMine)
            {
                Debug.Log($"OnDestroy : { photonView.gameObject.name }");

                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}
