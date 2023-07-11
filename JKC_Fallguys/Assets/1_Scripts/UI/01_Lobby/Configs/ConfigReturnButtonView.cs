using UnityEngine;
using UnityEngine.UI;

public class ConfigReturnButtonView : View
{
    public Button ActionButton { get; private set; }

    private void Awake()
    {
        ActionButton = transform.Find("ActionButton").GetComponent<Button>();
        Debug.Assert(ActionButton != null);
    }
}
