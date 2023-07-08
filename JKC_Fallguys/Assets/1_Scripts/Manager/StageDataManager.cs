using System;
using System.Collections.Generic;
using UniRx;

public class StageDataManager : SingletonMonoBehaviour<StageDataManager>
{
    protected override void Awake()
    {
        base.Awake();
        
        PlayerContainer.Initialize(gameObject);
    }

    public PlayerContainer PlayerContainer = new PlayerContainer();
    
    // 게임 시작의 카운트다운을 관장합니다.
    private ReactiveProperty<bool> _isGameStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsGameStart => _isGameStart;

    // 게임의 활성화 상태를 나타냅니다.
    private ReactiveProperty<bool> _isGameActive = new ReactiveProperty<bool>(false);

    public IReactiveProperty<bool> IsGameActive => _isGameActive;

    // // 현재 클라이언트를 플레이하고 있는 플레이어의 생존 여부를 나타냅니다.
    private Dictionary<int, ReactiveProperty<bool>> _isPlayerActiveDict = new Dictionary<int, ReactiveProperty<bool>>();
    public IReactiveProperty<bool> IsPlayerActive(int actorNumber)
    {
        if (!_isPlayerActiveDict.ContainsKey(actorNumber))
        {
            _isPlayerActiveDict[actorNumber] = new ReactiveProperty<bool>(true);
        }

        return _isPlayerActiveDict[actorNumber];
    }

    // 맵에서 쓰일 데이타가 저장되는 딕셔너리입니다. 
    public Dictionary<int, MapData> MapDatas = new Dictionary<int, MapData>();

    // 선택할 맵 데이터를 판별하는 객체입니다.
    public bool[] MapPickupFlags = new bool[ResourceManager.MaxPlayableMaps];
    private ReactiveProperty<int> _mapPickupIndex = new ReactiveProperty<int>();

    public IReactiveProperty<int> MapPickupIndex => _mapPickupIndex;

    public Dictionary<int, PlayerData> PlayerDataByIndex = new Dictionary<int, PlayerData>();

    // 결과 창에서 사용될 플레이어의 인덱스를 캐싱해놓는 리스트입니다.
    public List<int> CachedPlayerIndicesForResults = new List<int>();

    // 두 리스트는 스테이지가 넘어갈 때, 초기화됩니다.
    // 클리어에 실패한 사용자를 기록하는 리스트입니다
    public List<int> FailedClearStagePlayers = new List<int>();

    // 스테이지에서 사용될 순위를 기록하는 리스트입니다.
    public List<int> StagePlayerRankings = new List<int>();

    // Round Result Panel의 성공, 실패, 종료 여부를 설정하기 위한 변수입니다.
    public enum PlayerState
    {
        Default,
        Victory,
        Defeat,
        GameTerminated
    }

    // 클라이언트 별 PlayerState를 관리하는 Dictionary입니다.
    private Dictionary<int, ReactiveProperty<PlayerState>> _clientStates =
        new Dictionary<int, ReactiveProperty<PlayerState>>();

    public IReactiveProperty<PlayerState> GetCurrentState(int actorNumber)
    {
        if (!_clientStates.ContainsKey(actorNumber))
        {
            _clientStates[actorNumber] = new ReactiveProperty<PlayerState>();
        }

        return _clientStates[actorNumber];
    }

    // 라운드가 끝났는지 확인하기 위한 변수입니다.
    private ReactiveProperty<bool> _isRoundCompleted = new ReactiveProperty<bool>(false);
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

    public void SetPlayerState(int actorNumber, PlayerState state)
    {
        GetCurrentState(actorNumber).Value = state;
    }

    public void SetPlayerActive(int actorNumber, bool status)
    {
        IsPlayerActive(actorNumber).Value = status;
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
        for (int i = 0; i < ResourceManager.MaxPlayableMaps; ++i)
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

    private void OnDestroy()
    {
        PlayerContainer.OnRelease();
        PlayerContainer = default;
    }
}
