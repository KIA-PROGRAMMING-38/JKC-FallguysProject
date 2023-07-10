using UnityEngine;

public class ReturnButtonViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ReturnButtonView").GetComponent<ReturnButtonView>();
        Debug.Assert(View != null);
        Presenter = new ReturnButtonPresenter();
        Debug.Assert(Presenter != null);
    }
}