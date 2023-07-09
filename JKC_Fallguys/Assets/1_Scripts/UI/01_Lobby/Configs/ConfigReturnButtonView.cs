using UnityEngine;
using UnityEngine.UI;

public class ConfigReturnButtonView : View
{
    public GameObject Default { get; private set; }
    public Button ActionButton { get; private set; }

    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        ActionButton = transform.Find("ActionButton").GetComponent<Button>();
        Debug.Assert(ActionButton != null);
    }
}
