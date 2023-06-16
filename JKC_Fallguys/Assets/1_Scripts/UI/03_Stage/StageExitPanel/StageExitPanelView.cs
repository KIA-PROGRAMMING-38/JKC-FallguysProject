using UnityEngine.UI;

public class StageExitPanelView : View
{
    public Button ResumeButton { get; private set; }
    public Button ExitButton { get; private set; }

    private void Awake()
    {
        ResumeButton = transform.Find("ResumeButton").GetComponent<Button>();
        ExitButton = transform.Find("ExitButton").GetComponent<Button>();
    }
}
