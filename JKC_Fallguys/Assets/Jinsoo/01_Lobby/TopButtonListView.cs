using UnityEngine;
using UnityEngine.UI;

public class TopButtonListView : View
{
    public Button HomeButton { get; private set; }
    public Button CustomizeButton { get; private set; }

    private void Awake()
    {
        HomeButton = transform.Find("HomeButton").GetComponent<Button>();
        Debug.Assert(HomeButton != null);
        CustomizeButton = transform.Find("CustomizeButton").GetComponent<Button>();
        Debug.Assert(CustomizeButton != null);
    }
}
