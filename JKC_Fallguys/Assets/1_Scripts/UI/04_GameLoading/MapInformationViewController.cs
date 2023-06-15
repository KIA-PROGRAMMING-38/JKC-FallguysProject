using UnityEngine;

public class MapInformationViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("MapInformationView").GetComponent<MapInformationView>();
        Debug.Assert(View != null);
        Presenter = new MapInformationPresenter();
        Debug.Assert(Presenter != null);
    }
}
