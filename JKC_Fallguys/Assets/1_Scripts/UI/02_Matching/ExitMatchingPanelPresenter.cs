using Photon.Pun;
using UniRx;
using Model;

public class ExitMatchingPanelPresenter : Presenter
{
    private ExitMatchingPanelView _exitMatchingPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _exitMatchingPanelView = view as ExitMatchingPanelView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _exitMatchingPanelView.CancelButton
            .OnClickAsObservable()
            .Subscribe(_ => MatchingSceneModel.SetActiveEnterLobbyPanel(false))
            .AddTo(_compositeDisposable);
        
        _exitMatchingPanelView.CheckButton
            .OnClickAsObservable()
            .Subscribe(_ => MatchingSceneModel.RoomAdmissionStatus(false))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        MatchingSceneModel.IsExitMatching
            .Subscribe(isActive => GameObjectHelper.SetActiveGameObject(_exitMatchingPanelView.gameObject, isActive))
            .AddTo(_compositeDisposable);

        MatchingSceneModel.IsEnterPhotonRoom
            .Where(isActive => !isActive)
            .Skip(1)
            .Subscribe(_ => ReturnLobby())
            .AddTo(_compositeDisposable);
        
        MatchingSceneModel.IsActionPossible
            .Where(isActive => !isActive)
            .Subscribe(isActive => GameObjectHelper.SetActiveGameObject(_exitMatchingPanelView.gameObject, false))
            .AddTo(_compositeDisposable);
    }
    
    private void ReturnLobby()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnRelease()
    {
        _exitMatchingPanelView = default;
        _compositeDisposable.Dispose();
    }
}
