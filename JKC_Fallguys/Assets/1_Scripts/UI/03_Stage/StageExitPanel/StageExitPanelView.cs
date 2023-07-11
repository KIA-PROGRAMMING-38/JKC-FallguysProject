using UnityEngine;
using UnityEngine.UI;

public class StageExitPanelView : View
{
    public Button ResumeButton { get; private set; }
    public Button ExitButton { get; private set; }

    private void Awake()
    {
        ResumeButton = transform.Find("ResumeButton").GetComponent<Button>();
        Debug.Assert(ResumeButton != null);
        ExitButton = transform.Find("ExitButton").GetComponent<Button>();
        Debug.Assert(ExitButton != null);
    }
}
