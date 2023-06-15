using System;
using Model;
using UniRx;

public class RoundResultPresenter : Presenter
{
    private RoundResultView _roundResultView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _roundResultView = view as RoundResultView;
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        RoundResultSceneModel.PerformScoreRaise();
        
        Observable.Interval(TimeSpan.FromSeconds(1.0))
            .Take(3) // 원하는 횟수만큼 실행하도록 설정
            .Subscribe(_ =>
            {
                TweenUIPosition();
            });
    }

    private void TweenUIPosition()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        RoundResultSceneModel.FirstScore.SubscribeToText(_roundResultView.FirstScore).AddTo(_compositeDisposable);
        RoundResultSceneModel.SecondScore.SubscribeToText(_roundResultView.SecondScore).AddTo(_compositeDisposable);
        RoundResultSceneModel.ThirdScore.SubscribeToText(_roundResultView.ThirdScore).AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _roundResultView = default;
        _compositeDisposable.Dispose();
    }
}
