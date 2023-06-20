using UniRx;

public class HowToPlayPresenter : Presenter
{
    private HowToPlayView _howToPlayView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _howToPlayView = view as HowToPlayView;
        
        InitializeRx();
    }

    public override void OnRelease()
    {
        
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        
    }
}
