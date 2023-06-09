using UnityEngine;
using UnityEngine.UI;

public class EnterLobbyFromMatchingView : View
{
    public Button CancelButton { get; private set; }
    public Button CheckButton { get; private set; }

    private EnterLobbyFromMatchingViewController _enterLobbyFromMatchingViewController;
    public EnterLobbyFromMatchingViewController EnterLobbyFromMatchingViewController
    {
        get { return _enterLobbyFromMatchingViewController; }
    }

    private void Awake()
    {
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        Debug.Assert(CancelButton != null);
        CheckButton = transform.Find("CheckButton").GetComponent<Button>();
        Debug.Assert(CheckButton != null);
    }

    public void SetReference(EnterLobbyFromMatchingViewController enterLobbyFromMatchingViewController)
    {
        _enterLobbyFromMatchingViewController = enterLobbyFromMatchingViewController;
    }
}
