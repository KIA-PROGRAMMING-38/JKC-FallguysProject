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
        // 씬이 시작될 떄, Canvas Alpha가 줄어들어 자연스러운 연출이 되도록 실행합니다.
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        Model.GameLoadingSceneModel.IsWhitePanelActive
            .Where(isActive => isActive)
            .Subscribe(_ => IncreaseCanvasAlpha().Forget())
            .AddTo(_compositeDisposable);

        Model.GameLoadingSceneModel.IsWhitePanelActive
            .Where(isActive => !isActive)
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

        _whiteScreenView.ViewCanvasGroup.alpha = 1f;
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
        
        _whiteScreenView.ViewCanvasGroup.alpha = 0f;
    }
    
    public override void OnRelease()
    {
        _whiteScreenView = default;
        _compositeDisposable.Dispose();
    }
}
