using Photon.Pun;

public class FruitPooler : MonoBehaviourPunCallbacks
{
    public DefaultPool defaultPrefabPool { get; private set; } // 포톤에서 제공하는 오브젝트 풀

    private void Awake()
    {
        defaultPrefabPool = PhotonNetwork.PrefabPool as DefaultPool;
    }
}
