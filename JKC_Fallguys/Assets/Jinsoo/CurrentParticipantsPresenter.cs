using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CurrentParticipantsPresenter : Presenter
{
    private CurrentParticipantsView _currentParticipantsView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _currentParticipantsView = view as CurrentParticipantsView;
        
        InitializeRx();        
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        _currentParticipantsView = default;
        _compositeDisposable.Dispose();
    }
}
