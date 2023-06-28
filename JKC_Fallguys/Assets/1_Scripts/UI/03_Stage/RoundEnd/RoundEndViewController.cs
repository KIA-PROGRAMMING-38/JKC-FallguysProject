using UnityEngine;

public class RoundEndViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RoundEndView").GetComponent<RoundEndView>();
        Debug.Assert(View != null);
        Presenter = new RoundEndPresenter();
        Debug.Assert(Presenter != null);
    }
}
