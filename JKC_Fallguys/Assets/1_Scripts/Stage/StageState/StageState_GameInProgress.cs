public class StageState_GameInProgress : StageState
{
    protected override void AddToSequenceActionDictionary()
    {
        StageManager.Instance.StageDataManager.SequenceActionDictionary[StageDataManager.StageSequence.GameInProgress] = this;
    }

    public override void Action()
    {
        
    }
}
