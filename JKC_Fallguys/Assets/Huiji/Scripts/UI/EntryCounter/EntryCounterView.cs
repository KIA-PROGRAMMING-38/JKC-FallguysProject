using System;
using UnityEngine;
using UnityEngine.UI;

public class EntryCounterView : View
{
    public Text EnteredGoalPlayerCount { get; private set; }
    public Text TotalPlayerCount { get; private set; }

    private void Awake()
    {
        EnteredGoalPlayerCount = transform.Find("EnteredGoalPlayerCount").GetComponent<Text>();
        Debug.Assert(EnteredGoalPlayerCount != null);

        TotalPlayerCount = transform.Find("TotalPlayerCount").GetComponent<Text>();
        Debug.Assert(TotalPlayerCount != null);
    }
}
