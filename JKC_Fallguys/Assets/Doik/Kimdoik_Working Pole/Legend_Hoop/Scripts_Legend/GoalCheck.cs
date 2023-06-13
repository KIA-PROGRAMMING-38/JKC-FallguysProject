using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    public bool GoalInPlayer;

    private void Start()
    {
        GoalInPlayer = false;    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GoalInPlayer = true;
        }
    }
}
