using UnityEngine;

public class ConfigReturnButtonViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ConfigReturnButtonView").GetComponent<ConfigReturnButtonView>();
        Debug.Assert(View != null);
        Presenter = new ConfigReturnButtonPresenter();
        Debug.Assert(Presenter != null);
    }
}
