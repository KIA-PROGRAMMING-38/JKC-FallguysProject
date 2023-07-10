using System;
using Model;
using UniRx;
using UnityEngine;

public class RoundResultPresenter : Presenter
{
    private RoundResultView _roundResultView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _roundResultView = view as RoundResultView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        RoundResultSceneModel.PerformScoreRaise();
        SetPlayerId();
        
        Observable.Timer(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => MoveRankingUI(_roundResultView.FirstRankingText.rectTransform, _firstTextPos))
            .AddTo(_compositeDisposable);
        
        Observable.Timer(TimeSpan.FromSeconds(0.6f))
            .Subscribe(_ => MoveRankingUI(_roundResultView.SecondRankingText.rectTransform, _secondTextPos))
            .AddTo(_compositeDisposable);
        
        Observable.Timer(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ => MoveRankingUI(_roundResultView.ThirdRankingText.rectTransform, _thirdTextPos))
            .AddTo(_compositeDisposable);
    }

    private void SetActiveUI()
    {
        if (RoundResultSceneModel.FallguyRankings.Count == 1)
        {
            _roundResultView.SecondScore.gameObject.SetActive(false);
            _roundResultView.SecondRankingText.gameObject.SetActive(false);
            _roundResultView.ThirdScore.gameObject.SetActive(false);
            _roundResultView.ThirdRankingText.gameObject.SetActive(false);
        }
        else if (RoundResultSceneModel.FallguyRankings.Count == 2)
        {
            _roundResultView.ThirdScore.gameObject.SetActive(false);
            _roundResultView.ThirdRankingText.gameObject.SetActive(false);
        }
    }

    private void SetPlayerId()
    {
        for (int playerIndex = 0; playerIndex < RoundResultSceneModel.FallguyRankings.Count; ++playerIndex)
        {
            _roundResultView.PlayerIDs[playerIndex].text =
                RoundResultSceneModel.FallguyRankings[playerIndex].PlayerName;
        }
    }

    private Vector2 _firstTextPos = new Vector2(0.07f, 0.7f);
    private Vector2 _secondTextPos = new Vector2(0.07f, 0.45f);
    private Vector2 _thirdTextPos = new Vector2(0.07f, 0.23f);
    private void MoveRankingUI(RectTransform ui, Vector2 targetPos)
    {
        ui.MoveUIAtSpeed(targetPos, _roundResultView.RoundResultCanvasRect, 7000);
    }

    protected override void OnUpdatedModel()
    {
        SetActiveUI();
        
        RoundResultSceneModel.FirstScore.SubscribeToText(_roundResultView.FirstScore).AddTo(_compositeDisposable);
        RoundResultSceneModel.SecondScore.SubscribeToText(_roundResultView.SecondScore).AddTo(_compositeDisposable);
        RoundResultSceneModel.ThirdScore.SubscribeToText(_roundResultView.ThirdScore).AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _roundResultView = default;
        _compositeDisposable.Dispose();
    }
}
