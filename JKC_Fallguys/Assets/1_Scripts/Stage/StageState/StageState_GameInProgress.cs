public class StageState_GameInProgress : StageState
{
    protected override void AddToSequenceActionDictionary()
    {
        StageManager.Instance.ObjectRepository.SequenceActionDictionary[ObjectRepository.StageSequence.GameInProgress] = this;
    }

    public override void Action()
    {
        
    }
}
