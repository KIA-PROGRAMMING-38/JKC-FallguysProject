using UnityEngine;

public class HorizontalRendererView : View
{
    public GameObject SplashArtPooler { get; private set; }
    
    private void Awake()
    {
        SplashArtPooler = transform.Find("SplashArtPooler").gameObject;
        Debug.Assert(SplashArtPooler != null);
    }
}
