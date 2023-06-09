using UnityEngine;
using UnityEngine.UI;

public class CurrentParticipantsView : View
{
    public Text CurrentParticipantsCount { get; private set; }

    private void Awake()
    {
        CurrentParticipantsCount = transform.Find("CurrentParticipantsCount").GetComponent<Text>();
        Debug.Assert(CurrentParticipantsCount != null);
    }
}
