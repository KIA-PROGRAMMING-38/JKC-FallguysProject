using LiteralRepository;
using Model;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class EnterMatchingPresenter : Presenter
{
    private EnterMatchingView _enterMatchingView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _enterMatchingView = view as EnterMatchingView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _enterMatchingView.EnterMatchingButton
            .OnClickAsObservable()
            .Subscribe(_ => TryEnterMatchingStandby())
            .AddTo(_compositeDisposable);
    }

    private void TryEnterMatchingStandby()
    {
        if (PhotonNetwork.InLobby)
        {
            SceneChangeHelper.ChangeLocalScene(SceneIndex.MatchingStandby);
        }
        else
        {
            Debug.Log("Failed to enter matching standby: not currently in lobby");
            LobbySceneModel.SetPhotonLobbyWarmingPanelState(true);
        }
    }

    protected override void OnUpdatedModel()
    {
        Model.LobbySceneModel.CurrentLobbyState
            .Subscribe(state => GameObjectHelper.SetActiveGameObject(_enterMatchingView.gameObject, state == Model.LobbySceneModel.LobbyState.Home))
            .AddTo(_compositeDisposable);
    }
    
    public override void OnRelease()
    {
        _enterMatchingView = default;
        _compositeDisposable.Dispose();
    }
}
