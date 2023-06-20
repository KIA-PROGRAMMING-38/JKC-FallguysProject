using System;
using Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayPresenter : Presenter
{
    private HowToPlayView _howToPlayView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _howToPlayView = view as HowToPlayView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _howToPlayView.NextButton.OnClickAsObservable()
            .TakeWhile(_ => LobbySceneModel.HowToPlayImageIndex.Value < _howToPlayView.HowToPlayImage.Length -1) // 2
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(_ => LobbySceneModel.IncreaseImageIndex())
            .AddTo(_compositeDisposable);
        
        _howToPlayView.NextButton.OnClickAsObservable()
            .Where(_ => LobbySceneModel.HowToPlayImageIndex.Value == _howToPlayView.HowToPlayImage.Length - 1)
            .Subscribe(_ => LobbySceneModel.SetIsLastIndex(true))
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        var HowToPlayState = LobbySceneModel.LobbyState.HowToPlay;

        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => SetActiveHowToPlayPanel(state == HowToPlayState))
            .AddTo(_compositeDisposable);

        LobbySceneModel.HowToPlayImageIndex
            .Where(_ => LobbySceneModel.CurrentLobbyState.Value == LobbySceneModel.LobbyState.HowToPlay)
            .Where(index => index < _howToPlayView.HowToPlayImage.Length)
            .Subscribe(_ => _howToPlayView.HowToPlayImage[LobbySceneModel.HowToPlayImageIndex.Value - 1].FillAmountTween(0, 1))
            .AddTo(_compositeDisposable);

        LobbySceneModel.HowToPlayImageIndex
            .Subscribe(_ => UpdateText())
            .AddTo(_compositeDisposable);
        
        // LobbySceneModel.HowToPlayImageIndex
        //     .Where(index => index == _howToPlayView.HowToPlayImage.Length)
        //     .Subscribe(_ => ResetHowToPlayProgress())
        //     .AddTo(_compositeDisposable);

        LobbySceneModel.IsLastIndex
            .Where(islastIndex => islastIndex)
            .Subscribe(_ => ResetHowToPlayProgress())
            .AddTo(_compositeDisposable);
    }

    private readonly string[] _descriptionText = new[]
    {
        "쇼에 참가하려면 \"플레이\"를 누르세요. 여러분은 승리를 차지할 때까지 다른 참가자들과 함께 무작위로 선택된 라운드를\n" +
        "완주하고, 추격하고 추월해야 합니다!\n \n" +
        "실패하셨나요? 다시 시도해보세요. 카메라는 언제나 작동중입니다!",
        "점프, 다이빙, 잡기는 물론, 키보드로 이동하고 마우스로 카메라를 조정할 수 있습니다.",
        "끝입니다.\"플레이\"버튼을 누르면 쇼가 시작됩니다. 크라운을 잡으세요!"
    };
    private void UpdateText()
    {
        _howToPlayView.DescriptionText.text = _descriptionText[LobbySceneModel.HowToPlayImageIndex.Value];
        Debug.Log(LobbySceneModel.HowToPlayImageIndex.Value);
        Debug.Log($"텍스트 카운트 : {_descriptionText.Length}");
    }

    private void ResetHowToPlayProgress()
    {
        LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Settings);
        LobbySceneModel.ResetImageIndex();
        LobbySceneModel.SetIsLastIndex(false);
        foreach (Image image in _howToPlayView.HowToPlayImage)
        {
            image.fillAmount = 1;
        }
    }

    void SetActiveHowToPlayPanel(bool status)
    {
        _howToPlayView.gameObject.SetActive(status);
    }
    
    public override void OnRelease()
    {
        _howToPlayView = default;
        _compositeDisposable.Dispose();
    }
}
