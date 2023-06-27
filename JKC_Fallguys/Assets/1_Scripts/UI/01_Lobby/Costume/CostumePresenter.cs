using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Animations;

public class CostumePresenter : Presenter
{
    private CostumeView _costumeView;
    public override void OnInitialize(View view)
    {
        _costumeView = view as CostumeView;
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
        
    }
}
