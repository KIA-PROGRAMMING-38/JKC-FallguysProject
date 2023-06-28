using UniRx;
using UnityEngine;

public class ResultInStagePresenter : Presenter
{
    private ResultInStageView _resultInStageView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _resultInStageView = view as ResultInStageView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    private Vector2 _center = new Vector2(0.5f, 0.5f);
    protected override void OnUpdatedModel()
    {
        StageDataManager.Instance.CurrentState
            // .Skip(1) // 초기 Default로 설정된것은 Skip.
            .Subscribe(currentState =>
            {
                switch (currentState)
                {
                    case StageDataManager.PlayerState.Victory:
                        _resultInStageView.ResultText.text = "성공!";
                        break;
                    case StageDataManager.PlayerState.Defeat:
                        _resultInStageView.ResultText.text = "실패!";
                        break;
                    case StageDataManager.PlayerState.GameTerminated:
                        _resultInStageView.ResultText.text = "종료!";
                        break;
                    default:
                        Debug.Log("PlayerState를 설정해야 합니다.");
                        break;
                }

                _resultInStageView.ResultPanel.MoveUI(_center, _resultInStageView.CanvasRect, 0.5f);
                
            })
            .AddTo(_compositeDisposable);
    }

    
    public override void OnRelease()
    {
        _resultInStageView = default;
        _compositeDisposable.Dispose();
    }
}
