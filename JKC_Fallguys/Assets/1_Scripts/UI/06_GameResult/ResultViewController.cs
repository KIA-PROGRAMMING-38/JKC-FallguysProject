using Model;
using UnityEngine;

public class ResultViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ResultView").GetComponent<ResultView>();
        Debug.Assert(View != null);
        Presenter = new ResultPresenter();
        Debug.Assert(Presenter != null);
    }
}
