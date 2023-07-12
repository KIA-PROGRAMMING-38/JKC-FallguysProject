public class StageState_GameInProgress : StageState
{
    public override void AddToDictionary()
    {
        StageManager.Instance.StageDataManager.SequenceActionDictionary[StageDataManager.StageSequence.GameInProgress] = this;
    }

    public override void Action()
    {
        
    }
}
