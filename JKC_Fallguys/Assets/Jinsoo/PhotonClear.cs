using UnityEngine;
using Photon.Pun;

public class PhotonClear : MonoBehaviour
{
    private void OnDestroy()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        foreach (PhotonView photonView in PhotonNetwork.PhotonViewCollection)
        {
            Debug.Log($"OnDestroy : { photonView.gameObject.name }");

            // 현재 클라이언트가 소유권을 가지고 있는지 체크
            if (!ReferenceEquals(photonView.Owner, PhotonNetwork.LocalPlayer))
            {
                // 소유권이 마스터 클라이언트에게 없다면, 마스터 클라이언트에게 양도
                photonView.TransferOwnership(PhotonNetwork.MasterClient.ActorNumber);
            }

            // 그 후에 파괴
            PhotonNetwork.Destroy(photonView);
        }
    }
}
