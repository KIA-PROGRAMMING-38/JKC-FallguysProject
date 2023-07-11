using UnityEngine;
using UnityEngine.UI;

public class CustomizationView : View
{
    public Button Costume { get; private set; }

    private void Awake()
    {
        Costume = transform.Find("Costume").GetComponent<Button>();
        Debug.Assert(Costume != null);
    }
}
