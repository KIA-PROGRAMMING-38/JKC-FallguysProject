using UnityEngine;

public class GoalPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("GoalPanelView").GetComponent<GoalPanelView>();
        Debug.Assert(View != null);
        Presenter = new GoalPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
