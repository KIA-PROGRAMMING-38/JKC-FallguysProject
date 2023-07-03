using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObservedPlayerNamePresenter : Presenter
{
    private ObservedPlayerNameView _observedPlayerNameView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _observedPlayerNameView = view as ObservedPlayerNameView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnUpdatedModel()
    {
        throw new System.NotImplementedException();
    }
    
    public override void OnRelease()
    {
        _observedPlayerNameView = default;
        _compositeDisposable.Dispose();
    }
}
