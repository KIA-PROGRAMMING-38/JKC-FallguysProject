using Photon.Pun;
using UniRx;

public class EnterLobbyFromMatchingPresenter : Presenter
{
    private EnterLobbyFromMatchingView _enterLobbyFromMatchingView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterLobbyFromMatchingView = view as EnterLobbyFromMatchingView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _enterLobbyFromMatchingView.CancelButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.MatchingSceneModel.DeActiveEnterLobbyPanel())
            .AddTo(_compositeDisposable);
        
        _enterLobbyFromMatchingView.CheckButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.MatchingSceneModel.RoomAdmissionStatus(false))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        Model.MatchingSceneModel.IsEnterLobbyFromMatchingScene
            .Where(isActive => !isActive)
            .Subscribe(_ => DeActivateEnterLobbyPanel())
            .AddTo(_compositeDisposable);

        Model.MatchingSceneModel.IsEnterPhotonRoom
            .Where(isActive => !isActive)
            .Subscribe(_ => ReturnLobby())
            .AddTo(_compositeDisposable);
        
        Model.MatchingSceneModel.IsActionPossible
            .Where(isActive => !isActive)
            .Subscribe(_ => Model.MatchingSceneModel.DeActiveEnterLobbyPanel())
            .AddTo(_compositeDisposable);
    }
    
    private void DeActivateEnterLobbyPanel()
    {
        _enterLobbyFromMatchingView.EnterLobbyFromMatchingViewController.gameObject.SetActive(false);
    }

    private void ReturnLobby()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnRelease()
    {
        _enterLobbyFromMatchingView = default;
        _compositeDisposable.Dispose();
    }
}
