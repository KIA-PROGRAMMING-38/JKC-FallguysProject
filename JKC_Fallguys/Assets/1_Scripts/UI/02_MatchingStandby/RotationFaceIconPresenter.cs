using System;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class RotationFaceIconPresenter : Presenter
{
    private RotationFaceIconView _rotationFaceIconView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    private float _rotationSpeed = 200f;

    public override void OnInitialize(View view)
    {
        _rotationFaceIconView = view as RotationFaceIconView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .Subscribe(_ => LoadingEffectRotation())
            .AddTo(_compositeDisposable);
        
        if (PhotonNetwork.InRoom)
        {
            EnterRoom();
        }
        else
        {
            RetryEnterRoom();
        }
    }
    
    private void EnterRoom()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MatchingSceneModel.StartCount)
            .Subscribe(_ => SetCountDownText())
            .AddTo(_compositeDisposable);
        
        StartGameCountDown().Forget();
    }
    
    private void SetCountDownText()
    {
        Debug.Log(Model.MatchingSceneModel.StartCount);
        
        if (Model.MatchingSceneModel.StartCount.Value > 3)
        {
            _rotationFaceIconView.CurrentServerStateText.text = "플레이어를 기다리는 중...";
        }
        else
        {
            _rotationFaceIconView.CurrentServerStateText.text = "이제 게임이 시작됩니다!";
        }
    }
    
    private async UniTaskVoid StartGameCountDown()
    {
        while (Model.MatchingSceneModel.StartCount.Value > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            Model.MatchingSceneModel.DecreaseStartCount();
        }
    }
    
    private void RetryEnterRoom()
    {
        RetryJoinStream().Forget();
        Debug.Log("Entered the room yet...");
    }
    
    private async UniTaskVoid RetryJoinStream()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        OnUpdatedModel();
    }

    private void LoadingEffectRotation()
    {
        _rotationFaceIconView.LoadingEffect.rectTransform.Rotate(Vector3.back * _rotationSpeed * Time.deltaTime); 
    }
    
    public override void OnRelease()
    {
        _rotationFaceIconView = default;
        _compositeDisposable.Dispose();
    }
}
