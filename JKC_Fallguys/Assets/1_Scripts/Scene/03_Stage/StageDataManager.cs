using System.Collections.Generic;
using UniRx;

public class StageDataManager : SingletonMonoBehaviour<StageDataManager>
{
    public PlayerContainer PlayerContainer = new PlayerContainer(); 
    
    // 게임 시작의 카운트다운을 관장합니다.
    private ReactiveProperty<bool> _isGameStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsGameStart => _isGameStart;

    // 게임의 활성화 상태를 나타냅니다.
    private ReactiveProperty<bool> _isGameActive = new ReactiveProperty<bool>(false);
    public IReactiveProperty<bool> IsGameActive => _isGameActive;

    // 맵에서 쓰일 데이타가 저장되는 딕셔너리입니다. 
    public Dictionary<int, MapData> MapDatas = new Dictionary<int, MapData>();
    
    public bool[] MapPickupFlags = new bool[DataManager.MaxPlayableMaps];
    private ReactiveProperty<int> _mapPickupIndex = new ReactiveProperty<int>();
    public IReactiveProperty<int> MapPickupIndex => _mapPickupIndex;

    // 플레이어의 점수들이 계속해서 저장되는 딕셔너리입니다.
    public Dictionary<int, PlayerData> PlayerDataByIndex = new Dictionary<int, PlayerData>();
    // 결과 창에서 사용될 플레이어의 인덱스를 캐싱해놓는 리스트입니다.
    public List<int> CachedPlayerIndicesForResults = new List<int>();
    
    // 두 리스트는 스테이지가 넘어갈 때, 초기화됩니다.
    // 클리어에 실패한 사용자를 기록하는 리스트입니다
    public List<int> FailedClearStagePlayers = new List<int>();
    // 스테이지에서 사용될 순위를 기록하는 리스트입니다.
    public List<int> StagePlayerRankings = new List<int>();

    // 라운드가 끝났는지 확인하기 위한 변수입니다.
    private ReactiveProperty<bool> _isRoundCompleted = new ReactiveProperty<bool>();
    public IReactiveProperty<bool> IsRoundCompleted => _isRoundCompleted;
    
    public void AddPlayerToRanking(int playerIndex)
    {
        StagePlayerRankings.Add(playerIndex);
    }

    public void AddPlayerToFailedClearStagePlayers(int actorNumber)
    {
        FailedClearStagePlayers.Add(actorNumber);
    }

    /// <summary>
    /// 게임 상태를 변경하는 메소드입니다.
    /// </summary>
    /// <param name="status">status의 값에 따라 게임을 활성화하거나 비활성화합니다.</param>
    public void SetGameStatus(bool status)
    {
        _isGameActive.Value = status;
    }

    /// <summary>
    /// 라운드의 상태를 변경하는 메소드입니다.
    /// </summary>
    /// <param name="status">status의 값에 따라 라운드를 활성화하거나 비활성화합니다.</param>
    public void SetRoundState(bool status)
    {
        _isRoundCompleted.Value = status;
    }

    /// <summary>
    /// StateDataManager는 Singleton으로 구성되어 있습니다.
    /// 이 클래스는, Loading이 시작될 때 생성되고 Lobby로 돌아갈 경우 파괴되어야 합니다.
    /// 이를 위한 public 함수입니다.
    /// </summary>
    public void DestorySelf()
    {
        Destroy(gameObject);
    }

    public bool IsFinalRound()
    {
        int index = 0;
        for (int i = 0; i < DataManager.MaxPlayableMaps; ++i)
        {
            if (MapPickupFlags[i])
            {
                ++index;
            }
        }
        
        return index == 3;
    }
    
    public void SetMapPickupFlag(int index)
    {
        _mapPickupIndex.Value = index;
    }

    public void SetGameStart(bool status)
    {
        _isGameStart.Value = status;
    }
}
