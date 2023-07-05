using UnityEngine;

public class RemainingTimeViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RemainingTimeView").GetComponent<RemainingTimeView>();
        Debug.Assert(View != null);
        Presenter = new RemainingTimePresenter();
        Debug.Assert(Presenter != null);
    }
}
