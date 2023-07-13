using UnityEngine;
using UnityEngine.UI;

public class LobbyConnectionWarningPanelView : View
{
    public Button InformationTouchPanel { get; private set; }

    private void Awake()
    {
        InformationTouchPanel = transform.Find("LobbyConnectionWarningPanel").GetComponent<Button>();
        Debug.Assert(InformationTouchPanel != null);
    }
}
