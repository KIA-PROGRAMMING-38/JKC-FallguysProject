using Photon.Pun;
using UniRx;

public class ObservedPlayerNamePresenter : Presenter
{
    private ObservedPlayerNameView _observedPlayerNameView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _observedPlayerNameView = view as ObservedPlayerNameView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        StageDataManager.Instance.IsGameActive
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => SetActiveGameObject(false))
            .AddTo(_compositeDisposable);

        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageDataManager.Instance.GetCurrentState(actorNumber)
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(_ => SetActiveGameObject(true))
            .AddTo(_compositeDisposable);

    }

    private void SetActiveGameObject(bool status)
    {
        _observedPlayerNameView.Default.SetActive(status);
        _observedPlayerNameView.PlayerNameText.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _observedPlayerNameView = default;
        _compositeDisposable.Dispose();
    }
}
