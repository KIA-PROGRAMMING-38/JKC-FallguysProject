using System;
using Model;
using UniRx;

public class EntryCounterPresenter : Presenter
{
    private EntryCounterView _entryCounterView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _entryCounterView = view as EntryCounterView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        // EntryCounter 업데이트. 아래는 임시 코드다.
        Observable.Timer(TimeSpan.FromSeconds(1))
            .Subscribe(_ => StageSceneModel.IncreaseEnteredPlayerCount())
            .AddTo(_compositeDisposable);
        
        // TotalPlayerCounter 업데이트.
        StageSceneModel.SetTotalPlayerCount(1);
    }

    protected override void OnUpdatedModel()
    {
        StageSceneModel.EnteredGoalPlayerCount.SubscribeToText(_entryCounterView.EnteredGoalPlayerCount);
        StageSceneModel.TotalPlayerCount.SubscribeToText(_entryCounterView.TotalPlayerCount);
    }

    public override void OnRelease()
    {
        _entryCounterView = default;
        _compositeDisposable.Dispose();
    }
}
