using System;
using LiteralRepository;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;

    private bool _goalInPlayer;
    public bool GoalInPlayer => _goalInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagLiteral.Player))
        {
            _goalInPlayer = true;
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagLiteral.Player))
        {
            _goalInPlayer = false;
            OnPlayerExit?.Invoke();
        }
    }
} 