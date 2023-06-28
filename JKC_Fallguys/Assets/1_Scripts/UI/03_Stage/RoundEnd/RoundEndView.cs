using System;
using UnityEngine;

public class RoundEndView : View
{
    public RectTransform CanvasRect { get; private set; }
    public RectTransform RoundEndPanel { get; private set; }

    private void Awake()
    {
        CanvasRect = GetComponent<RectTransform>();
        RoundEndPanel = transform.Find("RoundEndPanel").GetComponent<RectTransform>();
        Debug.Assert(RoundEndPanel != null);
    }
}
