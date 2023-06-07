using UnityEngine;

public class LoginPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("LoginPanelView").GetComponent<LoginPanelView>();
        Debug.Assert(View != null);
        Presenter = new LoginPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
