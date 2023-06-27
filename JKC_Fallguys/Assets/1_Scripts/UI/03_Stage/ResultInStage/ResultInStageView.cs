
using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultInStageView : View
{
    public RectTransform CanvasRect { get; private set; }
    public RectTransform ResultPanel { get; private set; }
    public Text ResultText { get; private set; }
    
    private void Awake()
    {
        CanvasRect = GetComponent<RectTransform>();
        Debug.Assert(CanvasRect != null);
        ResultPanel = transform.Find("ResultPanel").GetComponent<RectTransform>();
        Debug.Assert(ResultPanel != null);
        ResultText = transform.Find("ResultPanel").Find("ResultText").GetComponent<Text>();
        Debug.Assert(ResultText != null);
    }
}
