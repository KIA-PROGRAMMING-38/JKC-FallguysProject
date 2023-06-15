using UnityEngine;

public class GameLoadingMainPanelView : View
{
    public GameObject Default { get; private set; }
    public GameObject Mask { get; private set; }
    
    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        Mask = transform.Find("Mask").gameObject;
        Debug.Assert(Mask != null);
    }
}
