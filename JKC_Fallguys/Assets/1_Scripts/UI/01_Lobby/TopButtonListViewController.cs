using UnityEngine;

public class TopButtonListViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("TopButtonListView").GetComponent<TopButtonListView>();
        Debug.Assert(View != null);
        Presenter = new TopButtonListPresenter();
        Debug.Assert(Presenter != null);
    }
}
