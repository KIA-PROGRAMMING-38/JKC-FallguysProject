using System.Collections.Generic;
using UniRx;

public class StageDataManager
{
    public static readonly int MaxPlayableMaps = 3;
    
    // 게임 시작의 카운트다운을 관장합니다.
    private ReactiveProperty<bool> _isGameStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsGameStart => _isGameStart;

    // 게임의 활성화 상태를 나타냅니다.
    private ReactiveProperty<bool> _isGameActive = new ReactiveProperty<bool>(false);
    public IReactiveProperty<bool> IsGameActive => _isGameActive;

    // 맵에서 쓰일 데이타가 저장되는 딕셔너리입니다. 
    public Dictionary<int, MapData> MapDatas = new Dictionary<int, MapData>();

    // 선택할 맵 데이터를 판별하는 객체입니다.
    public bool[] MapPickupFlags = new bool[MaxPlayableMaps];
    private ReactiveProperty<int> _mapPickupIndex = new ReactiveProperty<int>();

    public IReactiveProperty<int> MapPickupIndex => _mapPickupIndex;

    // 라운드가 끝났는지 확인하기 위한 변수입니다.
    private ReactiveProperty<bool> _isRoundCompleted = new ReactiveProperty<bool>(false);
    public IReactiveProperty<bool> IsRoundCompleted => _isRoundCompleted;
    
    public void SetGameStatus(bool status)
    {
        _isGameActive.Value = status;
    }

    public void SetRoundState(bool status)
    {
        _isRoundCompleted.Value = status;
    }

    public bool IsFinalRound()
    {
        int index = 0;
        for (int i = 0; i < MaxPlayableMaps; ++i)
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

    public void Clear()
    {
        SetGameStatus(false);
        SetRoundState(false);
        SetGameStart(false);
    }
}
