using System;
using UnityEngine;
using UnityEngine.UI;

public class GoalPanelView : View
{
    public Text GoalText { get; private set; }

    private void Awake()
    {
        GoalText = transform.Find("GoalText").GetComponent<Text>();
    }
}
