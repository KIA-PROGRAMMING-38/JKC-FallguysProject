using Model;
using UniRx;

public class RemainingTimePresenter : Presenter
{
    private RemainingTimeView _remainingTimeView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _remainingTimeView = view as RemainingTimeView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {

    }

    protected override void OnUpdatedModel()
    {
        StageSceneModel.RemainingTime
            .DistinctUntilChanged()
            .Subscribe(_ => SetReaminingTimeText())
            .AddTo(_compositeDisposable);
        
        StageManager.Instance.StageDataManager.IsGameActive
            .Skip(1)
            .Where(gameActive => !gameActive)
            .Subscribe(_ => GameObjectHelper.SetActiveGameObject(_remainingTimeView.gameObject, false))
            .AddTo(_compositeDisposable);
    }

    private void SetReaminingTimeText()
    {
        _remainingTimeView.RemainingTime.text = $"{StageSceneModel.RemainingTime.Value:D2}";
    }

    public override void OnRelease()
    {
        _remainingTimeView = default;
        _compositeDisposable.Dispose();
    }
}
