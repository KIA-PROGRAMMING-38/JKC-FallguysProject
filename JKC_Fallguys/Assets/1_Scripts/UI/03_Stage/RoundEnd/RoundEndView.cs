using System;
using UnityEngine;

public class RoundEndView : View
{
    public RectTransform RoundEndPanel { get; private set; }

    private void Awake()
    {
        RoundEndPanel = transform.Find("RoundEndPanel").GetComponent<RectTransform>();
        Debug.Assert(RoundEndPanel != null);
    }
}
