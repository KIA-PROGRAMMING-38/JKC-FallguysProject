using UnityEngine;

public class SettingsPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("SettingsPanelView").GetComponent<SettingsPanelView>();
        Debug.Assert(View != null);
        Presenter = new SettingsPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
