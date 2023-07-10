using UnityEngine;
using UnityEngine.UI;

public class EnterMatchingView : View
{
    public GameObject Default { get; private set; }
    public Button EnterMatchingButton { get; private set; }

    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        EnterMatchingButton = transform.Find("EnterMatchingButton").GetComponent<Button>();
        Debug.Assert(EnterMatchingButton != null);
    }
}
