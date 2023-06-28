using UniRx;
using UnityEngine;

public class RoundEndPresenter : Presenter
{
    private RoundEndView _roundEndView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _roundEndView = view as RoundEndView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    private Vector2 _center = new Vector2(0.5f, 0.5f);
    protected override void OnUpdatedModel()
    {
        StageDataManager.Instance.IsRoundCompleted
            .Where(isRoundCompleted => isRoundCompleted)
            .Subscribe(_ => _roundEndView.RoundEndPanel.MoveUI(_center, _roundEndView.CanvasRect, 0.5f))
            .AddTo(_compositeDisposable);
    }

    public override void OnRelease()
    {
        _roundEndView = default;
        _compositeDisposable.Dispose();
    }
}
