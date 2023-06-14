using UniRx;

public class HorizontalRendererPresenter : Presenter
{
    private HorizontalRendererView _horizontalRendererView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _horizontalRendererView = view as HorizontalRendererView;
        
        // InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnUpdatedModel()
    {
        throw new System.NotImplementedException();
    }
    
    public override void OnRelease()
    {
        _horizontalRendererView = default;
        _compositeDisposable.Dispose();
    }
}
