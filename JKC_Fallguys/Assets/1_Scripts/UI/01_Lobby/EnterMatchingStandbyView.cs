using UnityEngine;
using UnityEngine.UI;

public class EnterMatchingStandbyView : View
{
    public Button EnterMatchingStandbyButton { get; private set; }

    private void Awake()
    {
        EnterMatchingStandbyButton = transform.Find("EnterMatchingStandbyButton").GetComponent<Button>();
        Debug.Assert(EnterMatchingStandbyButton != null);
    }
}
