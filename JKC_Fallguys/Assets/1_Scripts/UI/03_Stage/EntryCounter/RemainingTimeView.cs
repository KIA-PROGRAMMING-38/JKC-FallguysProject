using UnityEngine;
using UnityEngine.UI;

public class RemainingTimeView : View
{
    public Text RemainingTime { get; private set; }

    private void Awake()
    {
        RemainingTime = transform.Find("RemainingTime").GetComponent<Text>();
        Debug.Assert(RemainingTime != null);
    }
}
