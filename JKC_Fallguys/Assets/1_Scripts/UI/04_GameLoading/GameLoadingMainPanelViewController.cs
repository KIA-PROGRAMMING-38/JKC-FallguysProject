using UnityEngine;

public class GameLoadingMainPanelViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("GameLoadingMainPanelView").GetComponent<GameLoadingMainPanelView>();
        Debug.Assert(View != null);
        Presenter = new GameLoadingMainPanelPresenter();
        Debug.Assert(Presenter != null);
    }
}
