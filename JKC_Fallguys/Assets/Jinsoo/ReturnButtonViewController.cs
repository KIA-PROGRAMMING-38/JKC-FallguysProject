using UnityEngine;

public class ReturnButtonViewController : ViewController
{
    public EnterLobbyFromMatchingViewController EnterLobbyFromMatchingViewController { private get; set; }
    
    private void Awake()
    {
        View = transform.Find("ReturnButtonView").GetComponent<ReturnButtonView>();
        Debug.Assert(View != null);
        Presenter = new ReturnButtonPresenter();
        Debug.Assert(Presenter != null);
    }

    protected override void Start()
    {
        base.Start();

        View.GetComponent<ReturnButtonView>().SetReference(EnterLobbyFromMatchingViewController);
    }

    public void OnInitialize
        (EnterLobbyFromMatchingViewController enterLobbyFromMatchingViewController)
    {
        EnterLobbyFromMatchingViewController = enterLobbyFromMatchingViewController;
    }
}