using UnityEngine;
using UnityEngine.UI;

public class RotationFaceIconView : View
{
    public Image LoadingEffect { get; private set; }
    public Text CurrentServerStateText { get; private set; }
    
    private void Awake()
    {
        LoadingEffect = transform.Find("LoadingEffect").GetComponent<Image>();
        Debug.Assert(LoadingEffect != null);
        CurrentServerStateText = transform.Find("CurrentServerStateText").GetComponent<Text>();
        Debug.Assert(CurrentServerStateText != null);
    }
}