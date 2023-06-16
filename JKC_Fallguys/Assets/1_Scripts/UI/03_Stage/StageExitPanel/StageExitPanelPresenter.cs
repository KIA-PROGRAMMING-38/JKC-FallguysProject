using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Model;
using UniRx;
using UnityEngine;

public class StageExitPanelPresenter : Presenter
{
    private StageExitPanelView _stageExitPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _stageExitPanelView = view as StageExitPanelView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        // 계속하기 버튼을 눌렀을때 입니다.
        _stageExitPanelView.ResumeButton
            .OnClickAsObservable()
            .Subscribe(_ => StageSceneModel.SetExitPanelActive(false))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        
    }
}
