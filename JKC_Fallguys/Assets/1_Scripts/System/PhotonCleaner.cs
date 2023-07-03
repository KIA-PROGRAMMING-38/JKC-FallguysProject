using UnityEngine;
using Photon.Pun;

public class PhotonCleaner : MonoBehaviourPun
{
    private void OnDestroy()
    {
        // IsMine 속성을 사용하여 이 오브젝트가 로컬 플레이어의 것인지 확인합니다.
        if (photonView.IsMine)
        {
            Debug.Log($"OnDestroy : { gameObject.name }");

            // 이 오브젝트를 파괴합니다.
            PhotonNetwork.Destroy(photonView);
        }
    }
}
