using UnityEngine;

public class EnterLobbyFromMatchingViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("EnterLobbyFromMatchingView").GetComponent<EnterLobbyFromMatchingView>();
        Debug.Assert(View != null);
        Presenter = new EnterLobbyFromMatchingPresenter();
        Debug.Assert(Presenter != null);
    }
    
    protected override void Start()
    {
        base.Start();

        View.GetComponent<EnterLobbyFromMatchingView>().SetReference(this);
    }
}
