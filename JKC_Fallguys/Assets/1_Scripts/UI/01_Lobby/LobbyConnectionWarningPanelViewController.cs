using UnityEngine;

public class LobbyConnectionWarningPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("LobbyConnectionWarningPanelView").GetComponent<LobbyConnectionWarningPanelView>();
        Debug.Assert(View != null);
        Presenter = new LobbyConnectionWarningPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
