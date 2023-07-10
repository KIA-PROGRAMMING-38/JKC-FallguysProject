using UnityEngine;
using UnityEngine.UI;

public class ExitMatchingPanelView : View
{
    public GameObject Default { get; private set; }
    public Button CancelButton { get; private set; }
    public Button CheckButton { get; private set; }

    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        Debug.Assert(CancelButton != null);
        CheckButton = transform.Find("CheckButton").GetComponent<Button>();
        Debug.Assert(CheckButton != null);
    }
}
