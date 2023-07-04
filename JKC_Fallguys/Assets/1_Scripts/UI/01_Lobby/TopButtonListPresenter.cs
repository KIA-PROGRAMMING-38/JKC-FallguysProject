using Model;
using UniRx;

public class TopButtonListPresenter : Presenter
{
    private TopButtonListView _topButtonListView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _topButtonListView = view as TopButtonListView;
        Model.LobbySceneModel.SetLobbyState(Model.LobbySceneModel.LobbyState.Home);
    
        InitializeRx();
    }

    /// <summary>
    /// 사용자 이벤트가 발생하면 LobbyDataModel의 LobbyState를 업데이트합니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _topButtonListView.HomeButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.LobbySceneModel.SetLobbyState(Model.LobbySceneModel.LobbyState.Home))
            .AddTo(_compositeDisposable);
        
        _topButtonListView.CustomizeButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.LobbySceneModel.SetLobbyState(Model.LobbySceneModel.LobbyState.Customization))
            .AddTo(_compositeDisposable);
    }
    
    protected override void OnUpdatedModel()
    {
        // TopButtonListView의 활성화 여부를 결정합니다.
        LobbySceneModel.CurrentLobbyState
        .Subscribe( state =>
        {
            if ( state == LobbySceneModel.LobbyState.Home || state == LobbySceneModel.LobbyState.Customization )
            {
                SetActiveTopButtonListView( true );
            }
            else
            {
                SetActiveTopButtonListView( false );
            }
        } )
        .AddTo( _compositeDisposable );

    }

    void SetActiveTopButtonListView( bool status )
    {
        _topButtonListView.gameObject.SetActive( status );
    }

    public override void OnRelease()
    {
        _topButtonListView = default;
        _compositeDisposable.Dispose();
    }
}
