using System;
using UnityEngine.UI;

public class HowToPlayView : View
{
    public Image[] HowToPlayImage { get; private set; } = new Image[3];
    public Text DescriptionText { get; private set; }
    
    public Button NextButton { get; private set; }

    private void Awake()
    {
        HowToPlayImage[0] = transform.Find("FirstImage").GetComponent<Image>();
        HowToPlayImage[1] = transform.Find("SecondImage").GetComponent<Image>(); 
        HowToPlayImage[2] = transform.Find("ThirdImage").GetComponent<Image>();
        DescriptionText = transform.Find("DescriptionText").GetComponent<Text>();
        NextButton = transform.Find("NextButton").GetComponent<Button>();
    }
}
