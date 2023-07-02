using Photon.Pun;

public class MapSelectionManager : MonoBehaviourPun
{
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SelectRandomMap();
        }
    }

    private int index = 0;
    private void SelectRandomMap()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (!StageDataManager.Instance.MapPickupFlags[index])
            {
                StageDataManager.Instance.MapPickupFlags[index] = true;
                StageDataManager.Instance.SetMapPickupFlag(index);
                
                // 마스터 클라이언트가 선택한 맵의 인덱스를 모든 클라이언트에게 보냅니다.
                photonView.RPC("SetSelectedMapIndex", RpcTarget.All, index);
            }
            else
            {
                ++index;

                SelectRandomMap();
            }
        }
    }

    [PunRPC]
    public void SetSelectedMapIndex(int mapIndex)
    {
        // 마스터 클라이언트가 선택한 맵의 인덱스를 설정합니다.
        StageDataManager.Instance.SetMapPickupFlag(mapIndex);
    }
}
