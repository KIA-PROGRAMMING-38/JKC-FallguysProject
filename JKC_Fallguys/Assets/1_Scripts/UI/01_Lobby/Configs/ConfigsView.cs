using UnityEngine;
using UnityEngine.UI;

public class ConfigsView : View
{
    public Slider MasterSlider { get; private set; }
    public Slider MusicSlider { get; private set; }
    public Slider SFXSlider { get; private set; }
    public Text ResolutionSettings { get; private set; }
    public Button ResolutionRightButton { get; private set; }
    public Button ResolutionLeftButton { get; private set;  }
    private void Awake()
    {
        MasterSlider = transform.Find( "MasterVolume" ).Find( "Slider" ).GetComponent<Slider>();
        Debug.Assert( MasterSlider != null );
        MusicSlider = transform.Find( "MasterVolume" ).Find( "Slider" ).GetComponent<Slider>();
        Debug.Assert( MusicSlider != null );
        SFXSlider = transform.Find( "SFXVolume" ).Find( "Slider" ).GetComponent<Slider>();
        Debug.Assert( SFXSlider != null );
        ResolutionSettings = transform.Find( "Resolution" ).Find( "ResolutionSettings" ).GetComponent<Text>();
        Debug.Assert( ResolutionSettings != null );
        ResolutionRightButton = transform.Find( "Resolution" ).Find( "RightButton" ).GetComponent<Button>();
        Debug.Assert( ResolutionRightButton != null );
        ResolutionLeftButton = transform.Find( "Resolution" ).Find( "LeftButton" ).GetComponent<Button>();
        Debug.Assert( ResolutionLeftButton != null );
    }
}
