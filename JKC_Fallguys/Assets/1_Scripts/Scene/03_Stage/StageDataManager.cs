using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StageDataManager : SingletonMonoBehaviour<StageDataManager>
{
    // 게임의 활성화 상태를 나타냅니다.
    private ReactiveProperty<bool> _isGameActive = new ReactiveProperty<bool>(false);
    public IReactiveProperty<bool> IsGameActive => _isGameActive;
    // 현재 클라이언트를 플레이하고 있는 플레이어의 생존 여부를 나타냅니다.
    private ReactiveProperty<bool> _isPlayerAlive = new ReactiveProperty<bool>(true);
    public IReactiveProperty<bool> IsPlayerAlive => _isPlayerAlive;

    // 맵에서 쓰일 데이타가 저장되는 딕셔너리입니다. 
    public Dictionary<int, MapData> MapDatas = new Dictionary<int, MapData>();
    // 선택할 맵 데이터를 판별하는 객체입니다.
    public bool[] MapPickupFlags = new bool[DataManager.MaxPlayableMaps];
    public int MapPickupIndex;
    // 플레이어의 점수들이 계속해서 저장되는 딕셔너리입니다.
    public Dictionary<int, PlayerData> PlayerDataByIndex = new Dictionary<int, PlayerData>();
    // 결과 창에서 사용될 플레이어의 인덱스를 캐싱해놓는 리스트입니다.
    public List<int> CachedPlayerIndicesForResults = new List<int>();
    // 스테이지에서 사용될 순위를 기록하는 리스트입니다.
    // 스테이지가 넘어갈 때, 초기화됩니다.
    public List<int> StagePlayerRankings = new List<int>();
    // Round Result Panel의 성공, 실패, 종료 여부를 설정하기 위한 변수입니다.
    public enum PlayerState
    {
        Default,
        Victory,
        Defeat,
        GameTerminated
    }

    private ReactiveProperty<PlayerState> _currentState = new ReactiveProperty<PlayerState>();
    public IReactiveProperty<PlayerState> CurrentState => _currentState;
    
    // 라운드가 끝났는지 확인하기 위한 변수입니다.
    private ReactiveProperty<bool> _isRoundCompleted = new ReactiveProperty<bool>();
    public IReactiveProperty<bool> IsRoundCompleted => _isRoundCompleted;
    
    public void AddPlayerToRanking(int playerIndex)
    {
        StagePlayerRankings.Add(playerIndex);

        foreach (int elem in StagePlayerRankings)
        {
            Debug.Log($"In PlayerIndexList, CurrentPlayer : {elem}");
        }
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
    /// StateDataManager는 Singleton으로 구성되어 있습니다.
    /// 이 클래스는, Loading이 시작될 때 생성되고 Lobby로 돌아갈 경우 파괴되어야 합니다.
    /// 이를 위한 public 함수입니다.
    /// </summary>
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}
