using UnityEngine;

public class RoundResultViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RoundResultView").GetComponent<RoundResultView>();
        Debug.Assert(View != null);
        Presenter = new RoundResultPresenter();
        Debug.Assert(Presenter != null);
    }
}
