using UnityEngine;
using UnityEngine.UI;

public class PurposePanelView : View
{
    public Text PurposeText { get; private set; }

    private void Awake()
    {
        PurposeText = transform.Find("PurposeText").GetComponent<Text>();
        Debug.Assert(PurposeText != null);
    }
}
