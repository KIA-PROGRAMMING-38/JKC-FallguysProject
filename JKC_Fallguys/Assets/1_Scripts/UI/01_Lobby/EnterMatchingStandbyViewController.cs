using UnityEngine;

public class EnterMatchingStandbyViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("EnterMatchingStandbyView").GetComponent<EnterMatchingStandbyView>();
        Debug.Assert(View != null);
        Presenter = new EnterMatchingStandbyPresenter();
        Debug.Assert(Presenter != null);
    }
}
