using Cysharp.Threading.Tasks;
using Model;
using UniRx;
using UnityEngine;

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
            .TakeWhile(_ => LobbySceneModel.HowToPlayImageIndex.Value < _howToPlayView.HowToPlayImage.Length)
            .Subscribe(_ => LobbySceneModel.IncreaseImageIndex())
            .AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        var HowToPlayState = LobbySceneModel.LobbyState.HowToPlay;

        LobbySceneModel.CurrentLobbyState
            .Subscribe(state => SetActiveHowToPlayPanel(state == HowToPlayState))
            .AddTo(_compositeDisposable);

        LobbySceneModel.HowToPlayImageIndex
            .Skip(1)
            .Subscribe(_ => _howToPlayView.HowToPlayImage[LobbySceneModel.HowToPlayImageIndex.Value - 1].FillAmountTween(0, 1))
            .AddTo(_compositeDisposable);

        LobbySceneModel.HowToPlayImageIndex
            .Where(index => index == _howToPlayView.HowToPlayImage.Length)
            .Subscribe(_ => LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Settings))
            .AddTo(_compositeDisposable);
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
