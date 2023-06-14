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

    protected override void OnOccuredUserEvent()
    {
        _exitButtonView.UIPopUpButton
            .OnClickAsObservable()
            .Subscribe(_ => StageSceneModel.ActiveExitPanel(true))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => StageSceneModel.IsExitPanelPopUp)
            .Where(_ => StageSceneModel.IsExitPanelPopUp)
            .Subscribe(_ => ActivateExitPanel())
            .AddTo(_compositeDisposable);
    }

    private void ActivateExitPanel()
    {
        _exitButtonView.StageExitPanel.gameObject.SetActive(true);
    }
    
    public override void OnRelease()
    {
        _exitButtonView = default;
        _compositeDisposable.Dispose();
    }
}
