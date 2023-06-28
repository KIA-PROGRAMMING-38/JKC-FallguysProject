using UnityEngine;

public class ResultInStageViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ResultInStageView").GetComponent<ResultInStageView>();
        Debug.Assert(View != null);
        Presenter = new ResultInStagePresenter();
        Debug.Assert(Presenter != null);
    }
}
