using UnityEngine;
using UnityEngine.UI;

public class EnterConfigView : View
{
    public Button EnterConfigButton { get; private set; }

    private void Awake()
    {
        EnterConfigButton = transform.Find("EnterConfigButton").GetComponent<Button>();
        Debug.Assert(EnterConfigButton != null);
    }
}
