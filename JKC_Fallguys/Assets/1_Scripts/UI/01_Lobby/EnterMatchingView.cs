using UnityEngine;
using UnityEngine.UI;

public class EnterMatchingView : View
{
    public Button EnterMatchingButton { get; private set; }

    private void Awake()
    {
        EnterMatchingButton = transform.Find("EnterMatchingButton").GetComponent<Button>();
        Debug.Assert(EnterMatchingButton != null);
    }
}
