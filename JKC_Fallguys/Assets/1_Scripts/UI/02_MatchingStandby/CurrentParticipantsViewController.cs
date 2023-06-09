using UnityEngine;

public class CurrentParticipantsViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("CurrentParticipantsView").GetComponent<CurrentParticipantsView>();
        Debug.Assert(View != null);
        Presenter = new CurrentParticipantsPresenter();
        Debug.Assert(Presenter != null);
    }
}
