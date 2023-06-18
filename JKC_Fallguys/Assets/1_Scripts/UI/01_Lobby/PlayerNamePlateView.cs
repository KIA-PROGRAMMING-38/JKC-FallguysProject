using UnityEngine;
using UnityEngine.UI;

public class PlayerNamePlateView : View
{
    public GameObject Default { get; private set; }
    public Text PlayerNameText { get; private set; }
    
    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        PlayerNameText = transform.Find("PlayerNameText").GetComponent<Text>();
        Debug.Assert(PlayerNameText != null);
    }
}
