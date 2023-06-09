using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonView : View
{
    public Button UIPopUpButton { get; private set; }

    
    private EnterLobbyFromMatchingViewController _enterLobbyFromMatchingViewController; 
    public EnterLobbyFromMatchingViewController EnterLobbyFromMatchingViewController
    {
        get { return _enterLobbyFromMatchingViewController; }
    }

    private void Awake()
    {
        UIPopUpButton = transform.Find("UIPopUpButton").GetComponent<Button>();
        Debug.Assert(UIPopUpButton != null);
    }
    
    public void SetReference
        (EnterLobbyFromMatchingViewController enterLobbyFromMatchingViewController)
    {
        _enterLobbyFromMatchingViewController = enterLobbyFromMatchingViewController;
    }
}
