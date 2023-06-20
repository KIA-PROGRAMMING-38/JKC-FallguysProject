using Model;
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

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        var HowToPlayState = LobbySceneModel.LobbyState.HowToPlay;

        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => SetActiveHowToPlayPanel(state == HowToPlayState))
            .AddTo(_compositeDisposable);
    }

    void SetActiveHowToPlayPanel(bool status)
    {
        _howToPlayView.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _howToPlayView = default;
        _compositeDisposable.Dispose();
    }
}
