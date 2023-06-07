/// <summary>
/// 비즈니스 로직을 정의할 Presenter.
/// </summary>
public abstract class Presenter
{
    /// <summary>
    /// View와 Presenter의 참조를 연결합니다.
    /// View에 기본값을 할당합니다.
    /// UniRx를 사용하기 때문에 반드시 InitializeRx를 호출해야합니다.
    /// </summary>
    /// <param name="view"></param>
    public abstract void OnInitialize(View view);

    /// <summary>
    /// View가 파괴될 때 호출됩니다.
    /// 자원정리 용도로 사용됩니다.
    /// </summary>
    public abstract void OnRelease();

    /// <summary>
    /// AbstractPresenter를 상속받은 클래스에서 실행할 함수입니다.
    /// </summary>
    protected void InitializeRx()
    {
        OnOccuredUserEvent();
        OnUpdatedModel();
    }

    /// <summary>
    /// View에 유저 이벤트가 발생했을 때 동작합니다.
    /// Model을 업데이트 합니다.
    /// </summary>
    protected abstract void OnOccuredUserEvent();

    /// <summary>
    /// Model이 업데이트 되었을 때 동작합니다.
    /// View를 업데이트 합니다.
    /// </summary>
    protected abstract void OnUpdatedModel();
}