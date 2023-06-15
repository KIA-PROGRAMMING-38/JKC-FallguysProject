using System;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;

    private bool goalInPlayer;

    public bool GoalInPlayer => goalInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("TriggerEnter");
            goalInPlayer = true;
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("TriggerExit");
            goalInPlayer = false;
            OnPlayerExit?.Invoke();
        }
    }
}