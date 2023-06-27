using UnityEngine;

public class PurposePanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("PurposePanelView").GetComponent<PurposePanelView>();
        Debug.Assert(View != null);
        Presenter = new PurposePanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
