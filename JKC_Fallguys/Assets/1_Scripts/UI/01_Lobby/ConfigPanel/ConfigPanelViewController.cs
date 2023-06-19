using UnityEngine;

public class ConfigPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ConfigPanelView").GetComponent<ConfigPanelView>();
        Debug.Assert(View != null);
        Presenter = new ConfigPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
