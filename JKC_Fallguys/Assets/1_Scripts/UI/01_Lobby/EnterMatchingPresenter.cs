using LiteralRepository;
using UniRx;
using UnityEngine.SceneManagement;

public class EnterMatchingPresenter : Presenter
{
    private EnterMatchingView _enterMatchingView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterMatchingView = view as EnterMatchingView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _enterMatchingView.EnterMatchingButton
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
        Model.LobbySceneModel.CurrentLobbyState
            .Where(state => state != Model.LobbySceneModel.LobbyState.Home)
            .Subscribe(_ => SetActiveGameObject(false))
            .AddTo(_compositeDisposable);
        
        Model.LobbySceneModel.CurrentLobbyState
            .Where(state => state == Model.LobbySceneModel.LobbyState.Home)
            .Subscribe(_ => SetActiveGameObject(true))
            .AddTo(_compositeDisposable);
    }

    private void SetActiveGameObject(bool status)
    {
        _enterMatchingView.Default.SetActive(status);
        _enterMatchingView.EnterMatchingButton.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _enterMatchingView = default;
        _compositeDisposable.Dispose();
    }
}
