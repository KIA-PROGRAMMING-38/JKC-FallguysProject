using UnityEngine;
using UnityEngine.UI;

public class CostumeView : View
{
    public GameObject Default { get; private set; }
    public GameObject ColorGroup { get; private set; }
    public Text ColorName { get; private set; }
    public Button ReturnButton { get; private set; }

    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        ColorGroup = transform.Find("ColorGroup").gameObject;
        Debug.Assert(ColorGroup != null);
        ColorName = transform.Find("ColorName").GetComponent<Text>();
        Debug.Assert(ColorName != null);
        ReturnButton = transform.Find("ReturnButton").GetComponent<Button>();
        Debug.Assert(ReturnButton != null);
    }
}
