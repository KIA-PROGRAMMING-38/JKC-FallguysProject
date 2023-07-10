using UnityEngine;

public class ExitMatchingPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ExitMatchingPanelView").GetComponent<ExitMatchingPanelView>();
        Debug.Assert(View != null);
        Presenter = new ExitMatchingPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
