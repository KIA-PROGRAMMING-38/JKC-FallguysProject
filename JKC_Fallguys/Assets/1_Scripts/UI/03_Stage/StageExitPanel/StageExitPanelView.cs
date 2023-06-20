using UnityEngine;
using UnityEngine.UI;

public class StageExitPanelView : View
{
    public GameObject Default { get; private set; }
    public Button ResumeButton { get; private set; }
    public Button ExitButton { get; private set; }

    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        ResumeButton = transform.Find("ResumeButton").GetComponent<Button>();
        Debug.Assert(ResumeButton != null);
        ExitButton = transform.Find("ExitButton").GetComponent<Button>();
        Debug.Assert(ExitButton != null);
    }
}
