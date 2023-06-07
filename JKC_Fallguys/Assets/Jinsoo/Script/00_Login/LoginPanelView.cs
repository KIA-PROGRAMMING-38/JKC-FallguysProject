using UnityEngine;
using UnityEngine.UI;

public class LoginPanelView : View
{
    public InputField PlayerNameInputField { get; private set; }
    public Button StartGameButton { get; private set; }
    public Button NameConventionGuideTouchPanel { get; private set; }
    
    private void Awake()
    {
        PlayerNameInputField = transform.Find("PlayerNameInputField").GetComponent<InputField>();
        Debug.Assert(PlayerNameInputField != null);
        StartGameButton = transform.Find("StartGameButton").GetComponent<Button>();
        Debug.Assert(StartGameButton != null);
        NameConventionGuideTouchPanel = transform.Find("NameConventionGuideTouchPanel").GetComponent<Button>();
        Debug.Assert(NameConventionGuideTouchPanel != null);
    }
}
