using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class StageDataManager
{
    public static readonly int MaxPlayableMaps = 3;

    public enum StageSequence
    {
        WaitingForPlayers,    
        PlayersReady,         
        GameInProgress,       
        GameCompletion,       
        RoundCompletion,      
        Transitioning         
    }

    private ReactiveProperty<StageSequence> _currentSequence = new ReactiveProperty<StageSequence>();
    public IReactiveProperty<StageSequence> CurrentSequence => _currentSequence;
    
    
    public Dictionary<StageSequence, StageState> SequenceActionDictionary = new Dictionary<StageSequence, StageState>();

    public void SetSequence(StageSequence sequence)
    {
        _currentSequence.Value = sequence;
    }

    // 맵에서 쓰일 데이타가 저장되는 딕셔너리입니다. 
    public Dictionary<int, MapData> MapDatas = new Dictionary<int, MapData>();

    // 선택할 맵 데이터를 판별하는 객체입니다.
    public bool[] MapPickupFlags = new bool[MaxPlayableMaps];
    
    private ReactiveProperty<int> _mapPickupIndex = new ReactiveProperty<int>();
    public IReactiveProperty<int> MapPickupIndex => _mapPickupIndex;

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
    
    public void Clear()
    {
        SetSequence(StageSequence.WaitingForPlayers);
    }
}
