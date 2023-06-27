using Photon.Pun;
using UnityEngine;

public class MapSelectionManager : MonoBehaviourPun
{
    private void Start()
    {
        // 마스터 클라이언트만 맵을 선택합니다.
        if (PhotonNetwork.IsMasterClient)
        {
            SelectRandomMap();
        }
    }

    private void SelectRandomMap()
    {
        int randomMapIndex = UnityEngine.Random.Range(0, DataManager.MaxPlayableMaps);

        if (!StageDataManager.Instance.MapPickupFlags[randomMapIndex])
        {
            StageDataManager.Instance.MapPickupFlags[randomMapIndex] = true;
            StageDataManager.Instance.MapPickupIndex = randomMapIndex;

            // 마스터 클라이언트가 선택한 맵의 인덱스를 모든 클라이언트에게 보냅니다.
            photonView.RPC("SetSelectedMapIndex", RpcTarget.All, randomMapIndex);
            
            Debug.Log(randomMapIndex);
        }
        else
        {
            // 만약 뽑힌 맵이 이미 선택된 맵이라면, 다시 맵을 선택합니다.
            SelectRandomMap();
        }
    }

    [PunRPC]
    public void SetSelectedMapIndex(int mapIndex)
    {
        // 마스터 클라이언트가 선택한 맵의 인덱스를 설정합니다.
        StageDataManager.Instance.MapPickupIndex = mapIndex;
    }
}
