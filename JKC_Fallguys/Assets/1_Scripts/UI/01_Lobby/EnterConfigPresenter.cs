using UniRx;
using UnityEngine;

public class EnterConfigPresenter : Presenter
{
    private EnterConfigView _enterConfigView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterConfigView = view as EnterConfigView;
        Model.LobbyDataModel.SetActiveConfigView(false);
        
        InitializeRx();
    }

    /// <summary>
    /// 환경 설정 UI를 표시하기 위한 버튼 이벤트입니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _enterConfigView.EnterConfigButton
            .OnClickAsObservable()
            .Subscribe(_ => Model.LobbyDataModel.SetActiveConfigView(true))
            .AddTo(_compositeDisposable);
    }

    /// <summary>
    /// 버튼 클릭을 감지하고 해당 이벤트를 처리합니다.
    /// </summary>
    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.LobbyDataModel.IsConfigurationRunning)
            .Where(_ => Model.LobbyDataModel.IsConfigurationRunning)
            .Subscribe(_ => Debug.Log("환경설정창이 실행됩니다."))
            .AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _enterConfigView = default;
        _compositeDisposable.Dispose();
    }
}
