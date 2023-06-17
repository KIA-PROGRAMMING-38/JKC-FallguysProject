using UnityEngine;

public class StageExitPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("StageExitPanelView").GetComponent<StageExitPanelView>();
        Debug.Assert(View != null);
        Presenter = new StageExitPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
