using UnityEngine;

public class RoundEndPresenter : Presenter
{
    private RoundEndView _roundEndView;
    public override void OnInitialize(View view)
    {
        _roundEndView = view as RoundEndView;
        
        InitializeRx();
    }

    public override void OnRelease()
    {
        
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        
    }
}
