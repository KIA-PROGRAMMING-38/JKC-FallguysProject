public class PurposePanelPresenter : Presenter
{
    private PurposePanelView _purposePanelView;
    
    public override void OnInitialize(View view)
    {
        _purposePanelView = view as PurposePanelView;
        
        InitializeRx();
    }
    
    protected override void OnOccuredUserEvent()
    {
        
    }

    protected override void OnUpdatedModel()
    {
        SetData();
    }

    private void SetData()
    {
        MapData mapData = StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex];

        _purposePanelView.PurposeText.text = mapData.Info.Purpose;
    }
    
    public override void OnRelease()
    {
        
    }
}
