
using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultInStageView : View
{
    public Text ResultText { get; private set; }
    private void Awake()
    {
        ResultText = transform.Find("Background").Find("ResultText").GetComponent<Text>();
        Debug.Assert(ResultText != null);
    }
}
