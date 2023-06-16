using Model;
using UniRx;

public class ExitButtonPresenter : Presenter
{
    private ExitButtonView _exitButtonView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _exitButtonView = view as ExitButtonView;
        
        InitializeRx();
    }

    /// <summary>
    /// 버튼을 클릭할 수 있는 상태가 되고 나서 버튼을 클릭해야 패널을 띄울 수 있습니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _exitButtonView.UIPopUpButton
            .OnClickAsObservable()
            .Where(_ => StageSceneModel.CanClickButton)
            .Subscribe(_ => StageSceneModel.SetExitPanelActive(true))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        StageSceneModel.IsExitPanelPopUp.DistinctUntilChanged()
            .Subscribe(value =>
            {
                if (value == true)
                {
                    SetExitPanelActive(true);
                }

                else
                {
                    SetExitPanelActive(false);
                }
            });
    }

    /// <summary>
    /// Exit Stage Panel의 활성화 여부를 설정합니다.
    /// </summary>
    /// <param name="status"></param>
    private void SetExitPanelActive(bool status)
    {
        _exitButtonView.StageExitPanel.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _exitButtonView = default;
        _compositeDisposable.Dispose();
    }
}
