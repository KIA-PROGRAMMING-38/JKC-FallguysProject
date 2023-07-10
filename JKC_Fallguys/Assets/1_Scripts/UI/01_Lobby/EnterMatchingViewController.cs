using UnityEngine;

public class EnterMatchingViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("EnterMatchingView").GetComponent<EnterMatchingView>();
        Debug.Assert(View != null);
        Presenter = new EnterMatchingPresenter();
        Debug.Assert(Presenter != null);
    }
}
