using Model;
using UniRx;
using Util.Helper;

public class EnterConfigPresenter : Presenter
{
    private EnterConfigView _enterConfigView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterConfigView = view as EnterConfigView;

        InitializeRx();
    }

    /// <summary>
    /// 환경 설정 UI를 표시하기 위한 버튼 이벤트입니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _enterConfigView.EnterConfigButton
            .OnClickAsObservable()
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Settings))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        LobbySceneModel.CurrentLobbyState
            .Subscribe( state => GameObjectHelper.SetActiveGameObject(_enterConfigView.gameObject, state == LobbySceneModel.LobbyState.Home ) )
            .AddTo( _compositeDisposable );
    }

    public override void OnRelease()
    {
        _enterConfigView = default;
        _compositeDisposable.Dispose();
    }
}
