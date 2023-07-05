using Model;
using UniRx;

public class RemainingTimePresenter : Presenter
{
    private RemainingTimeView _remainingTimeView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _remainingTimeView = view as RemainingTimeView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {

    }

    protected override void OnUpdatedModel()
    {
        StageSceneModel.RemainingTime.SubscribeToText(_remainingTimeView.RemainingTime);
    }

    public override void OnRelease()
    {
        _remainingTimeView = default;
        _compositeDisposable.Dispose();
    }
}
