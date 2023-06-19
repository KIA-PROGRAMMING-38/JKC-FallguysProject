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
        ConfigsButton = transform.Find("ConfigsButton").GetComponent<Button>();
        HowToPlayButton = transform.Find("HowToPlayButton").GetComponent<Button>();
        GameExitButton = transform.Find("GameExitButton").GetComponent<Button>();
        ClosePanelButton = transform.Find("ClosePanelButton").GetComponent<Button>();
    }
}
