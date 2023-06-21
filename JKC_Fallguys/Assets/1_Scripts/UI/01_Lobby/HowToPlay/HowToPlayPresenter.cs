using System;
using Model;
using UniRx;
using UnityEngine.UI;

public class HowToPlayPresenter : Presenter
{
    private HowToPlayView _howToPlayView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    private int _buttonClickCount = 0;
    public override void OnInitialize(View view)
    {
        _howToPlayView = view as HowToPlayView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _howToPlayView.NextButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                LobbySceneModel.IncreaseImageIndex();
                _buttonClickCount++;

                if (_buttonClickCount >= 3)
                {
                    ResetHowToPlayProgress();
                }
            })
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        var HowToPlayState = LobbySceneModel.LobbyState.HowToPlay;

        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => SetActiveHowToPlayPanel(state == HowToPlayState))
            .AddTo(_compositeDisposable);

        // Image를 업데이트 합니다.
        LobbySceneModel.HowToPlayImageIndex
            .Where(_ => LobbySceneModel.CurrentLobbyState.Value == LobbySceneModel.LobbyState.HowToPlay)
            .Where(index => index < _howToPlayView.HowToPlayImage.Length)
            .Subscribe(_ => _howToPlayView.HowToPlayImage[LobbySceneModel.HowToPlayImageIndex.Value - 1].FillAmountTween(0, 1))
            .AddTo(_compositeDisposable);

        // 텍스트를 Update 합니다.
        LobbySceneModel.HowToPlayImageIndex
            .DistinctUntilChanged()
            .Where(_ => _buttonClickCount < 2)
            .Subscribe(_ => UpdateText())
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

    private int _textIndex;
    private void UpdateText()
    {
        _howToPlayView.DescriptionText.text = _descriptionText[_textIndex];
        ++_textIndex;
    }

    private void ResetHowToPlayProgress()
    {
        LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Settings);
        LobbySceneModel.ResetImageIndex();
        foreach (Image image in _howToPlayView.HowToPlayImage)
        {
            image.fillAmount = 1;
        }
        
        _buttonClickCount = 0; // 클릭 횟수 초기화
        _textIndex = 0;
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
