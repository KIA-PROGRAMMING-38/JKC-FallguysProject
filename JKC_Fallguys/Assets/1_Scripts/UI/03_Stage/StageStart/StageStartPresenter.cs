using System;
using Model;
using UniRx;
using UnityEngine;

public class StageStartPresenter : Presenter
{
    private StageStartView _stageStartView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _stageStartView = view as StageStartView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        SubTitlePopUpAnimation();
        
        // Game Start가 되면 실행되야 한다.
        Observable.Interval(TimeSpan.FromSeconds(1.5f))
            .Subscribe(_ => CountdownUIAnimation())
            .AddTo(_compositeDisposable);
    }

    private Vector2 _subTitleTargetPos = new Vector2(0.3f,0.2f);
    private void SubTitlePopUpAnimation()
    {
        _stageStartView.SubTitle.rectTransform.MoveUI(_subTitleTargetPos, _stageStartView.StageStartCanvasRect, 1f)
            .SetEase(Ease.EaseOutElastic);
    }

    private Vector2 _outPos = new Vector2(-0.5f, 0.2f);
    private void AnimateTitleOut()
    {
        _stageStartView.Title.rectTransform.MoveUI(_outPos, _stageStartView.StageStartCanvasRect, 0.5f)
            .SetEase(Ease.Linear);
        _stageStartView.SubTitle.rectTransform.MoveUI(_outPos, _stageStartView.StageStartCanvasRect, 0.5f)
            .SetEase(Ease.Linear);
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

            _stageStartView.CountdownImage.sprite = sprite;

            ++_spriteIndex;
            
            ScaleAnimation();
        }
        else
        {
            _spriteIndex = 0;
            _stageStartView.CountdownImage.gameObject.SetActive(false);
            AnimateTitleOut();

            StageDataManager.Instance.SetGameStatus(true);
            // 카운트다운 후에 Exit 버튼을 누를 수 있게 합니다.
            StageSceneModel.SetExitButtonActive(true);
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

        _stageStartView.CountdownImage.transform.localScale = _vectorZero;
        _stageStartView.CountdownImage.transform.ScaleTween(_vectorOne * targetScale, duration)
            .SetEase(Ease.EaseOutElastic);            
    }

    protected override void OnUpdatedModel()
    {
        SetData();
    }

    private void SetData()
    {
        MapData mapData = StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex.Value];

        _stageStartView.TitleText.text = mapData.Info.MapName;
        _stageStartView.SubTitleText.text = mapData.Info.Description;
    }
    
    public override void OnRelease()
    {
        _stageStartView = default;
        _compositeDisposable.Dispose();
    }
}
