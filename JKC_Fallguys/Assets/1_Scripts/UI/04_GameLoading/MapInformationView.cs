using UnityEngine;
using UnityEngine.UI;

public class MapInformationView : View
{
    public GameObject Default { get; private set; }
    public Text MapNameText { get; private set; }
    public GameObject MapSplashArtMask { get; private set; }
    public Image MapSplashArtImage { get; private set; }
    public Text PlayExplanation { get; private set; }
    
    private void Awake()
    {
        Default = transform.Find("Default").gameObject;
        Debug.Assert(Default != null);
        MapNameText = transform.Find("MapNameText").GetComponent<Text>();
        Debug.Assert(MapNameText != null);
        MapSplashArtMask = transform.Find("MapSplashArtMask").gameObject;
        Debug.Assert(MapSplashArtMask != null);
        MapSplashArtImage = transform.Find("MapSplashArtMask").transform.Find("SpriteImage").GetComponent<Image>();
        Debug.Assert(MapSplashArtImage != null);
        PlayExplanation = transform.Find("PlayExplanation").GetComponent<Text>();
        Debug.Assert(PlayExplanation != null);
    }
}
