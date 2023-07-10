using Model;
using Photon.Pun;
using UniRx;

public class StageExitPanelPresenter : Presenter
{
    private StageExitPanelView _stageExitPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _stageExitPanelView = view as StageExitPanelView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _stageExitPanelView.ResumeButton
            .OnClickAsObservable()
            .Subscribe(_ => StageSceneModel.SetExitPanelActive(false))
            .AddTo(_compositeDisposable);

        _stageExitPanelView.ExitButton
            .OnClickAsObservable()
            .Subscribe(_ => StageSceneModel.RoomAdmissionStatus(false))
            .AddTo(_compositeDisposable);
        }

    protected override void OnUpdatedModel()
    {
        StageSceneModel.IsEnterPhotonRoom
            .Where(isActive => !isActive)
            .Subscribe(_ => ReturnLobby())
            .AddTo(_compositeDisposable);
        
        StageSceneModel.IsExitPanelPopUp
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_stageExitPanelView.gameObject, state))
            .AddTo(_compositeDisposable);
    }
 
    private void ReturnLobby()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnRelease()
    {
        _stageExitPanelView = default;
        _compositeDisposable.Dispose();
    }
}
