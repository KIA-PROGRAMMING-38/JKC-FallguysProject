using UnityEngine;
using Util.Helper;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    public PlayerRepository PlayerRepository { get; private set; }
    public GameObject ObjectRepository { get; private set; }

    public StageDataManager StageDataManager = new StageDataManager();
    public PlayerContainer PlayerContainer = new PlayerContainer();

    public void Initialize()
    {
        MakeRepository();
    }

    public void Clear()
    {
        StageDataManager.Clear();
        PlayerContainer.Clear();
    }

    private void MakeRepository()
    {
        PlayerRepository = GameObjectHelper.CreateRepository<PlayerRepository>("PlayerRepository", gameObject.transform);
        ObjectRepository = GameObjectHelper.CreateRepository("ObjectRepository", gameObject.transform);
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
