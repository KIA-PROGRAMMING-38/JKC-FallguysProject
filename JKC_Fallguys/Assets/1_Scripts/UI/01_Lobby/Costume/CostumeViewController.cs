using UnityEngine;

public class CostumeViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("CostumeView").GetComponent<CostumeView>();
        Debug.Assert(View != null);
        Presenter = new CostumePresenter();
        Debug.Assert(Presenter != null);
    }
}
