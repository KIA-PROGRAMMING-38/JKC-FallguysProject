using UnityEngine;

public class WhiteScreenViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("WhiteScreenView").GetComponent<WhiteScreenView>();
        Debug.Assert(View != null);
        Presenter = new WhiteScreenPresenter();
        Debug.Assert(Presenter != null);
    }
}
