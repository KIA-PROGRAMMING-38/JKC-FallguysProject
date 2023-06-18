using UnityEngine;

public class CustomizationViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("CustomizationView").GetComponent<CustomizationView>();
        Debug.Assert(View != null);
        Presenter = new CustomizationPresenter();
        Debug.Assert(Presenter != null);
    }
}
