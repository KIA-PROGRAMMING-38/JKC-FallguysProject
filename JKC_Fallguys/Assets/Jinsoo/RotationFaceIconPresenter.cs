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
