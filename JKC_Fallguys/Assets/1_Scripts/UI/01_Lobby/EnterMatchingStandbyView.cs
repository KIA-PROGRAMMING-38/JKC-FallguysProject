using UnityEngine;
using UnityEngine.UI;

public class EnterMatchingStandbyView : View
{
    public GameObject Default { get; private set; }
    public Button EnterMatchingStandbyButton { get; private set; }

    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        EnterMatchingStandbyButton = transform.Find("EnterMatchingStandbyButton").GetComponent<Button>();
        Debug.Assert(EnterMatchingStandbyButton != null);
    }
}
