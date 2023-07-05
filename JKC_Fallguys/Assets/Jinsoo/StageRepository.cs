using System;

public class StageRepository : SingletonMonoBehaviour<StageRepository>
{
    public event Action OnPlayerDispose;
    
    public void Initialize()
    {
        
    }

    public void PlayerDispose()
    {
        OnPlayerDispose?.Invoke();        
    }
}
