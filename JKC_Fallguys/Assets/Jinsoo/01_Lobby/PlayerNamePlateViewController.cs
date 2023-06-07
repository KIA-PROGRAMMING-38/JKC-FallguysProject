using UnityEngine;

public class PlayerNamePlateViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("PlayerNamePlateView").GetComponent<PlayerNamePlateView>();
        Debug.Assert(View != null);
        Presenter = new PlayerNamePlatePresenter();
        Debug.Assert(Presenter != null);
    }
}
