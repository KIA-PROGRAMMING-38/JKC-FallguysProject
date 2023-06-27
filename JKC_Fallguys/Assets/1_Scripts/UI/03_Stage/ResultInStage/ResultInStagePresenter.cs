using UnityEngine;

public class ResultInStagePresenter : Presenter
{
    private ResultInStageView _resultInStageView;
    public override void OnInitialize(View view)
    {
        _resultInStageView = view as ResultInStageView;
        
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
