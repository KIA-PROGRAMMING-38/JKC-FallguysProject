using System;
using UniRx;
using UnityEngine;

public class CountdownPresenter : Presenter
{
    private CountdownView _countdownView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _countdownView = view as CountdownView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        // Game Start가 되면 실행되야 한다.
        Observable.Interval(TimeSpan.FromSeconds(1.5f))
            .Subscribe(_ => CountdownUIAnimation())
            .AddTo(_compositeDisposable);
    }

    private int _spriteIndex;
    /// <summary>
    /// Countdown Sprite를 다 바꿔끼운다음에 Image를 비활성화 시킵니다. 
    /// </summary>
    private void CountdownUIAnimation()
    {
        if (_spriteIndex < CountdownSpritesRegistry.Sprites.Count)
        {
            Sprite sprite = CountdownSpritesRegistry.Sprites[_spriteIndex];

            _countdownView.CountdownImage.sprite = sprite;

            ++_spriteIndex;
            
            ScaleAnimation();
        }

        else
        {
            _spriteIndex = 0;
            _countdownView.CountdownImage.gameObject.SetActive(false);
        }
    }
    
    private Vector3 _vectorZero = Vector3.zero;
    private Vector3 _vectorOne = Vector3.one;
    /// <summary>
    /// Image UI Scale을 키우는 애니메이션입니다.
    /// </summary>
    private void ScaleAnimation()
    {
        float targetScale = 1.0f;
        float duration = 1.5f;

        _countdownView.CountdownImage.transform.localScale = _vectorZero;
        _countdownView.CountdownImage.transform.ScaleTween(_vectorOne * targetScale, duration)
            .SetEase(Ease.EaseOutElastic);            
    }

    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        _countdownView = default;
        _compositeDisposable.Dispose();
    }
}
