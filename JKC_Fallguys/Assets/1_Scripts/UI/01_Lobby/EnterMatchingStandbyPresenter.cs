using LiteralRepository;
using UniRx;
using UnityEngine.SceneManagement;

public class EnterMatchingStandbyPresenter : Presenter
{
    private EnterMatchingStandbyView _enterMatchingStandbyView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterMatchingStandbyView = view as EnterMatchingStandbyView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _enterMatchingStandbyView.EnterMatchingStandbyButton
            .OnClickAsObservable()
            .Subscribe(_ => TryEnterMatchingStandby())
            .AddTo(_compositeDisposable);
    }

    private void TryEnterMatchingStandby()
    {
        SceneManager.LoadScene(SceneIndex.MatchingStandby);
    }

    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        _enterMatchingStandbyView = default;
        _compositeDisposable.Dispose();
    }
}
