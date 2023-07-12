using Photon.Pun;

public abstract class StageState : MonoBehaviourPun
{
    protected void Awake()
    {
        AddToDictionary();
    }

    public abstract void AddToDictionary();
    
    public abstract void Action();
}
