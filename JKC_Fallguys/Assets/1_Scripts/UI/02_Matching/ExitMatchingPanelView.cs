using UnityEngine;
using UnityEngine.UI;

public class ExitMatchingPanelView : View
{
    public Button CancelButton { get; private set; }
    public Button CheckButton { get; private set; }

    private void Awake()
    {
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        Debug.Assert(CancelButton != null);
        CheckButton = transform.Find("CheckButton").GetComponent<Button>();
        Debug.Assert(CheckButton != null);
    }
}
