using System;
using UnityEngine;

public class PlayerRepository : MonoBehaviour
{
    public event Action OnPlayerDispose;
    
    public void PlayerDispose()
    {
        OnPlayerDispose?.Invoke();        
    }
}
