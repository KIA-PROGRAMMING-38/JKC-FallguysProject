using UnityEngine;
using UnityEngine.UI;

public class ObservedPlayerNameView : View
{
    public Text PlayerNameText { get; private set; }

    private void Awake()
    {
        PlayerNameText = transform.Find("PlayerNameText").GetComponent<Text>();
        Debug.Assert(PlayerNameText != null);
    }
}
