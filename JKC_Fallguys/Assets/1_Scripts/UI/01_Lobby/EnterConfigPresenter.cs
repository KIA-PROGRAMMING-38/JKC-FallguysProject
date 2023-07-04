using Model;
using UniRx;
using UnityEngine;

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

    /// <summary>
    /// 버튼 클릭을 감지하고 해당 이벤트를 처리합니다.
    /// </summary>
    protected override void OnUpdatedModel()
    {
        // EnterSettginsView의 활성화 여부를 설정합니다.
        LobbySceneModel.CurrentLobbyState
            .Subscribe( state => SetActiveEnterSettingsView( state == LobbySceneModel.LobbyState.Home ) )
            .AddTo( _compositeDisposable );
    }

    void SetActiveEnterSettingsView( bool status )
    {
        _enterConfigView.gameObject.SetActive( status );
    }

    public override void OnRelease()
    {
        _enterConfigView = default;
        _compositeDisposable.Dispose();
    }
}
