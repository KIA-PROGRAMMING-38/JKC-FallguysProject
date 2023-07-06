using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelView : View
{
    public Image BackgroundImage { get; private set; }
    public Button ConfigsButton { get; private set; }
    public Button HowToPlayButton { get; private set; }
    public Button GameExitButton { get; private set; }
    
    public Button ClosePanelButton { get; private set; }
    private void Awake()
    {
        BackgroundImage = transform.Find("BackgroundImage").GetComponent<Image>();
        Debug.Assert(BackgroundImage != null);
        ConfigsButton = transform.Find("ConfigsButton").GetComponent<Button>();
        Debug.Assert(ConfigsButton != null);
        HowToPlayButton = transform.Find("HowToPlayButton").GetComponent<Button>();
        Debug.Assert(HowToPlayButton != null);
        GameExitButton = transform.Find("GameExitButton").GetComponent<Button>();
        Debug.Assert(GameExitButton != null);
        ClosePanelButton = transform.Find("ClosePanelButton").GetComponent<Button>();
        Debug.Assert(ClosePanelButton != null);
    }
}
