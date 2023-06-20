using UnityEngine;
using UnityEngine.UI;

public class ExitButtonView : View
{
    public Button UIPopUpButton { get; private set; }

    private void Awake()
    {
        UIPopUpButton = transform.Find("UIPopUpButton").GetComponent<Button>();
        Debug.Assert(UIPopUpButton != null);
    }
}
