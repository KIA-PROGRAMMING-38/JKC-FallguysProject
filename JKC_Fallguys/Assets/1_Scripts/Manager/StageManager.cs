using Photon.Pun;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    public PlayerRepository PlayerRepository { get; private set; }
    public GameObject ObjectRepository { get; private set; }

    public StageDataManager StageDataManager = new StageDataManager();
    public PlayerContainer PlayerContainer = new PlayerContainer();
    public PhotonTimeHelper PhotonTimeHelper;

    protected override void Awake()
    {
        base.Awake();

        MakeRepository();
    }

    private void Start()
    {
        PhotonTimeHelper = FindObjectOfType<PhotonTimeHelper>();
    }

    public void Clear()
    {
        StageDataManager.Clear();
        PlayerContainer.SetPlayerActive(PhotonNetwork.LocalPlayer.ActorNumber, true);

        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        PlayerContainer.SetPlayerState(actorNumber, PlayerContainer.PlayerState.Default);
    }

    public string[] objectName = { "PlayerRepository", "ObjectRepository" };

    private void MakeRepository()
    {
        GameObject playerRepository = new GameObject(objectName[0]);
        playerRepository.AddComponent<PlayerRepository>();
        playerRepository.transform.position = Vector3.zero;
        playerRepository.transform.rotation = Quaternion.identity;
        playerRepository.transform.SetParent(gameObject.transform);
        PlayerRepository = playerRepository.GetComponent<PlayerRepository>();

        GameObject objectRepository = new GameObject(objectName[1]);
        objectRepository.transform.position = Vector3.zero;
        objectRepository.transform.rotation = Quaternion.identity;
        objectRepository.transform.SetParent(gameObject.transform);
        ObjectRepository = objectRepository;
    }

    /// StateDataManager는 Singleton으로 구성되어 있습니다.
    /// 이 클래스는, Loading이 시작될 때 생성되고 Lobby로 돌아갈 경우 파괴되어야 합니다.
    /// 이를 위한 public 함수입니다.
    /// </summary>
    public void DestorySelf()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PlayerContainer = default;
        StageDataManager = default;
    }
}
