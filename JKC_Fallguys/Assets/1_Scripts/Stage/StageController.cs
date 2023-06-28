using Photon.Pun;
using UniRx;

/// <summary>
/// 맵의 게임 시간을 설정하고 게임 진행에 관련된 일을 처리합니다.
/// 각 스테이지에 대한 정보와 상태를 관리하는 역할을 합니다.
/// 순위가 아닌, 캐릭터의 승리와 패배를 관리합니다.
/// </summary>
public abstract class StageController : MonoBehaviourPun
{
    // 스테이지의 플레이 시간을 체크하는 ReactiveProperty입니다.
    protected ReactiveProperty<int> remainingGameTime = new ReactiveProperty<int>();
    
    protected virtual void Awake()
    {
        SetGameTime();
        InitializeRx();
    }

    /// <summary>
    /// 스테이지의 게임 시간을 설정합니다.
    /// </summary>
    protected abstract void SetGameTime();
    
    /// <summary>
    /// 시간 경과에 대한 설정을 UniRx로 구현합니다.
    /// </summary>
    protected abstract void InitializeRx();
}
