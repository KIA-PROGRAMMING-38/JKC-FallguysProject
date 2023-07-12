using Photon.Pun;

public abstract class StageState : MonoBehaviourPun
{
    protected void Awake()
    {
        AddToSequenceActionDictionary();
    }

    protected abstract void AddToSequenceActionDictionary();
    
    public abstract void Action();
}
