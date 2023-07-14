using System.Collections.Generic;
using Photon.Pun;
using UniRx;

public class PlayerContainer
{
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
    
    public Dictionary<int, PlayerData> PlayerDataByIndex = new Dictionary<int, PlayerData>();

    // 결과 창에서 사용될 플레이어의 인덱스를 캐싱해놓는 리스트입니다.
    public List<int> CachedPlayerIndicesForResults = new List<int>();

    // 두 리스트는 스테이지가 넘어갈 때, 초기화됩니다.
    // 스테이지에서 클리어에 실패한 사용자를 기록하는 리스트입니다
    public List<int> FailedClearStagePlayers = new List<int>();
    // 스테이지에서의 순위를 기록하는 리스트입니다.
    public List<int> StagePlayerRankings = new List<int>();

    public enum PlayerState
    {
        Default,
        Victory,
        Defeat,
        GameTerminated
    }
    
    // 클라이언트 별 PlayerState를 관리하는 Dictionary입니다.
    private Dictionary<int, ReactiveProperty<PlayerState>> _clientStates = new Dictionary<int, ReactiveProperty<PlayerState>>();
    public IReactiveProperty<PlayerState> GetCurrentState(int actorNumber)
    {
        if (!_clientStates.ContainsKey(actorNumber))
        {
            _clientStates[actorNumber] = new ReactiveProperty<PlayerState>();
        }

        return _clientStates[actorNumber];
    }
    
    public void AddPlayerToRanking(int playerIndex)
    {
        StagePlayerRankings.Add(playerIndex);
    }

    public void AddFailedPlayer(int actorNumber)
    {
        FailedClearStagePlayers.Add(actorNumber);
    }
    
    public void SetPlayerState(int actorNumber, PlayerState state)
    {
        GetCurrentState(actorNumber).Value = state;
    }

    public void SetPlayerActive(int actorNumber, bool status)
    {
        IsPlayerActive(actorNumber).Value = status;
    }

    public void Clear()
    {
        SetPlayerActive(PhotonNetwork.LocalPlayer.ActorNumber, true);

        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        SetPlayerState(actorNumber, PlayerContainer.PlayerState.Default);
        
        StagePlayerRankings.Clear();
        FailedClearStagePlayers.Clear();
    }
}
