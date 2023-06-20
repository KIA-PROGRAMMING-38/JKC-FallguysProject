using System;
using UnityEngine.UI;

public class HowToPlayView : View
{
    public Image FirstImage { get; private set; }
    public Image SecondImage { get; private set; }
    public Image ThirdImage { get; private set; }
    public Text DescriptionText { get; private set; }

    private void Awake()
    {
        FirstImage = transform.Find("FirstImage").GetComponent<Image>();
        SecondImage = transform.Find("SecondImage").GetComponent<Image>();
        ThirdImage = transform.Find("ThirdImage").GetComponent<Image>();
        DescriptionText = transform.Find("DescriptionText").GetComponent<Text>();
    }
}
