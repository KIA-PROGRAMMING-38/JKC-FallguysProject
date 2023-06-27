using UnityEngine;
using UnityEngine.UI;

public class StageStartView : View
{
    public Image CountdownImage { get; private set; }
    public Image SubTitle { get; private set; }
    public Text SubTitleText { get; private set; }
    public Image Title { get; private set; }
    public Text TitleText { get; private set; }
    public RectTransform StageStartCanvasRect { get; private set; }

    private void Awake()
    {
        CountdownImage = transform.Find("CountdownImage").GetComponent<Image>();
        CountdownImage.gameObject.transform.localScale = Vector3.zero;
        Debug.Assert(CountdownImage != null);
        StageStartCanvasRect = GetComponent<RectTransform>();
        Debug.Assert(StageStartCanvasRect != null);
        SubTitle = transform.Find("SubTitle").GetComponent<Image>();
        Debug.Assert(SubTitle != null);
        SubTitleText = SubTitle.transform.Find("SubTitleText").GetComponent<Text>();
        Debug.Assert(SubTitleText != null);
        Title = transform.Find("Title").GetComponent<Image>();
        Debug.Assert(Title != null);
        TitleText = Title.transform.Find("TitleText").GetComponent<Text>();
        Debug.Assert(TitleText != null);
    }
}
