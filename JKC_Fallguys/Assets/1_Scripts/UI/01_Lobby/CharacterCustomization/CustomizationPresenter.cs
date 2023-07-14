using Model;
using UniRx;
using Util.Helper;

public class CustomizationPresenter : Presenter
{
    private CustomizationView _customizationView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _customizationView = view as CustomizationView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _customizationView.Costume
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Costume));
    }

    protected override void OnUpdatedModel()
    {
        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_customizationView.gameObject, state == LobbySceneModel.LobbyState.Customization))
            .AddTo(_compositeDisposable);
    }

    public override void OnRelease()
    {
        _customizationView = default;
        _compositeDisposable.Dispose();
    }
}
