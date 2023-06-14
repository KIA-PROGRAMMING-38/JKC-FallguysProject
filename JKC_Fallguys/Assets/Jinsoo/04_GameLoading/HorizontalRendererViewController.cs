using UnityEngine;

public class HorizontalRendererViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("HorizontalRendererView").GetComponent<HorizontalRendererView>();
        Debug.Assert(View != null);
        Presenter = new HorizontalRendererPresenter();
        Debug.Assert(Presenter != null);
    }
}
