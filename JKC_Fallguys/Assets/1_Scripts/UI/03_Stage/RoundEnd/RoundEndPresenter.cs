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
        StageManager.Instance.ObjectRepository.CurrentSequence
            .Where(sequence => sequence == ObjectRepository.StageSequence.RoundCompletion)
            .Subscribe(_ => UIAction())
            .AddTo(_compositeDisposable);
    }

    private void UIAction()
    {
        _roundEndView.RoundEndPanel.MoveUI(_center, _roundEndView.CanvasRect, 0.5f)
            .SetEase(Ease.EaseOutElastic);
    }

    public override void OnRelease()
    {
        _roundEndView = default;
        _compositeDisposable.Dispose();
    }
}
