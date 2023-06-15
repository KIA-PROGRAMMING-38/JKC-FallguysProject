using Photon.Pun;
using UniRx;

public class CurrentParticipantsPresenter : Presenter
{
    private CurrentParticipantsView _currentParticipantsView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public override void OnInitialize(View view)
    {
        _currentParticipantsView = view as CurrentParticipantsView;

        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        Model.MatchingSceneModel.IsEnterPhotonRoom
            .Where(isActive => isActive)
            .Subscribe(_ => OnActiveCurrentPlayerCount())
            .AddTo(_compositeDisposable);
    }

    private void OnActiveCurrentPlayerCount()
    {
        _currentParticipantsView.CurrentParticipantsCount.gameObject.SetActive(true);

        Observable.EveryUpdate()
            .Subscribe(_ => UpdateCurrentPlayerCountText())
            .AddTo(_compositeDisposable);
    }

    // 현재 참여자 수를 업데이트하는 메서드입니다.
    private void UpdateCurrentPlayerCountText()
    {
        int playerCount = PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.PlayerCount : 0;
        _currentParticipantsView.CurrentParticipantsCount.text = $"{playerCount}명의 플레이어와 매치 발견!";
    }

    public override void OnRelease()
    {
        _currentParticipantsView = default;
        _compositeDisposable.Dispose();
    }
}