using UniRx;

public class GoalPanelPresenter : Presenter
{
    private GoalPanelView _goalPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _goalPanelView = view as GoalPanelView;
        
        InitializeRx();
    }
    
    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        
    }
}
