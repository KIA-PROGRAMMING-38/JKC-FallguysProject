using UnityEngine;

public class GameLoadingMainPanelView : View
{
    public GameObject Mask { get; private set; }
    
    private void Awake()
    {
        Mask = transform.Find("Mask").gameObject;
        Debug.Assert(Mask != null);
    }
}
