using UnityEngine;

public class WhiteScreenView : View
{
    public CanvasGroup ViewCanvasGroup { get; private set; }

    private void Awake()
    {
        ViewCanvasGroup = GetComponent<CanvasGroup>();
        Debug.Assert(ViewCanvasGroup != null);
    }
}
