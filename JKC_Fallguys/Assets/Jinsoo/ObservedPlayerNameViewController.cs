using UnityEngine;

public class ObservedPlayerNameViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ObservedPlayerNameView").GetComponent<ObservedPlayerNameView>();
        Debug.Assert(View != null);
        Presenter = new ObservedPlayerNamePresenter();
        Debug.Assert(Presenter != null);
    }
}
