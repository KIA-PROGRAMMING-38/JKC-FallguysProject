using UnityEngine;

public class EnterConfigViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("EnterConfigView").GetComponent<EnterConfigView>();
        Debug.Assert(View != null);
        Presenter = new EnterConfigPresenter();
        Debug.Assert(Presenter != null);
    }
}
