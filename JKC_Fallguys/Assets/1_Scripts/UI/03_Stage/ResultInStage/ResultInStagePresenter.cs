using Cysharp.Threading.Tasks;
using Photon.Pun;
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
    
    protected override void OnUpdatedModel()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log($"ResultInActorNumber : {PhotonNetwork.LocalPlayer.ActorNumber}");
        StageDataManager.Instance.GetCurrentState(actorNumber)
            .Skip(1) // 초기 Default로 설정된것은 Skip.
            .DistinctUntilChanged()
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

                UIAnimation().Forget();
            
            })
            .AddTo(_compositeDisposable);
    }
    
    private Vector2 _center = new Vector2(0.5f, 0.5f);
    private Vector2 _outPosition = new Vector2(-1, 0.5f);
    private async UniTaskVoid UIAnimation()
    {
        _resultInStageView.ResultPanel.MoveUI(_center, _resultInStageView.CanvasRect, 0.5f)
            .SetEase(Ease.EaseOutElastic);

        await UniTask.Delay(3000);
        
        _resultInStageView.ResultPanel.MoveUI(_outPosition, _resultInStageView.CanvasRect, 0.5f);
    }

    
    public override void OnRelease()
    {
        _resultInStageView = default;
        _compositeDisposable.Dispose();
    }
}
