using UnityEngine;

/// <summary>
/// 화면의 가장 기본이 되는 클래스
/// </summary>
public class ViewController : MonoBehaviour
{
    protected View View { get; set; }
    protected Presenter Presenter { get; set; }

    /// <summary>
    /// ViewController를 상속받은 클래스에서 Start 호출
    /// ViewController를 상속받은 클래스는 반드시 Awake에서 Presenter와 View의 참조를 연결해주어야 한다
    /// </summary>
    protected void Start()
    {
        Presenter.OnInitialize(View);
    }

    /// <summary>
    /// 신이 전환될 때는 ViewController를 상속받은 객체가 파괴될 것
    /// 파괴 될 때의 자원 정리를 위해 OnRelease 호출
    /// </summary>
    protected void OnDestroy()
    {
        Presenter.OnRelease();
    }
}