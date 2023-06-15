using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class WhiteScreenPresenter : Presenter
{
    private WhiteScreenView _whiteScreenView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _whiteScreenView = view as WhiteScreenView;
        DecreaseCanvasAlpha().Forget();
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        DeActiveRandomPickUIEffect().Forget();
    }

    private async UniTaskVoid DeActiveRandomPickUIEffect()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(10f));

        Model.GameLoadingSceneModel.SetActiveWhitePanel(true);
    }
    

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.GameLoadingSceneModel.IsWhitePanelActive)
            .Where(_ => Model.GameLoadingSceneModel.IsWhitePanelActive)
            .Subscribe(_ => IncreaseCanvasAlpha().Forget())
            .AddTo(_compositeDisposable);
        
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.GameLoadingSceneModel.IsWhitePanelActive)
            .Where(_ => !Model.GameLoadingSceneModel.IsWhitePanelActive)
            .Subscribe(_ => DecreaseCanvasAlpha().Forget())
            .AddTo(_compositeDisposable);
    }

    private async UniTaskVoid IncreaseCanvasAlpha()
    {
        float duration = 1.0f;
        float elapsedTime = 0f;
        
        while (_whiteScreenView.ViewCanvasGroup.alpha < 0.99f)
        {
            elapsedTime += Time.deltaTime;
            _whiteScreenView.ViewCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);

            await UniTask.Yield();
        }
    }
    
    private async UniTaskVoid DecreaseCanvasAlpha()
    {
        float duration = 1.0f;
        float elapsedTime = 0f;
        
        while (_whiteScreenView.ViewCanvasGroup.alpha > 0.01f)
        {
            elapsedTime += Time.deltaTime;
            _whiteScreenView.ViewCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);

            await UniTask.Yield();
        }
    }
    
    public override void OnRelease()
    {
        _whiteScreenView = default;
        _compositeDisposable.Dispose();
    }
}
