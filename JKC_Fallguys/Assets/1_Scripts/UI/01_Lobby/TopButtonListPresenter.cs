using UniRx;
using UnityEngine;

public class TopButtonListPresenter : Presenter
{
    private TopButtonListView _topButtonListView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _topButtonListView = view as TopButtonListView;
        Model.LobbySceneModel.SetLobbyState(Model.LobbySceneModel.CurrentLobbyState.Home);
    
        InitializeRx();
    }

    /// <summary>
    /// 사용자 이벤트가 발생하면 LobbyDataModel의 LobbyState를 업데이트합니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _topButtonListView.HomeButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.LobbySceneModel.SetLobbyState(Model.LobbySceneModel.CurrentLobbyState.Home))
            .AddTo(_compositeDisposable);
        
        _topButtonListView.CustomizeButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.LobbySceneModel.SetLobbyState(Model.LobbySceneModel.CurrentLobbyState.Customization))
            .AddTo(_compositeDisposable);
    }
    
    /// <summary>
    /// LobbyState가 변경될 때 마다 해당하는 이벤트를 실행하는 구독을 설정합니다.
    /// </summary>
    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.LobbySceneModel.LobbyState)
            .Where(_ => Model.LobbySceneModel.LobbyState == Model.LobbySceneModel.CurrentLobbyState.Home)
            .Subscribe(_ => Debug.Log("홈 UI가 실행됩니다."))
            .AddTo(_compositeDisposable);
        
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.LobbySceneModel.LobbyState)
            .Where(_ => Model.LobbySceneModel.LobbyState == Model.LobbySceneModel.CurrentLobbyState.Customization)
            .Subscribe(_ => Debug.Log("커스터마이즈 UI가 실행됩니다."))
            .AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _topButtonListView = default;
        _compositeDisposable.Dispose();
    }
}
