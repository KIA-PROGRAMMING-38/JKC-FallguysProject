using UnityEngine;
using UnityEngine.UI;

public class CustomizationView : View
{
    public GameObject Default { get; private set; }
    public Button Costume { get; private set; }

    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        Costume = transform.Find("Costume").GetComponent<Button>();
        Debug.Assert(Costume != null);
    }
}
