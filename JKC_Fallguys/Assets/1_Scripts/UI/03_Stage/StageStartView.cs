using UnityEngine;
using UnityEngine.UI;

public class StageStartView : View
{
    public Image CountdownImage { get; private set; }
    public Image Title { get; private set; }
    public Image SubTitle { get; private set; }
    public RectTransform StageStartCanvasRect { get; private set; }
    private Vector3 _zero = Vector3.zero;

    private void Awake()
    {
        CountdownImage = transform.Find("CountdownImage").GetComponent<Image>();
        CountdownImage.gameObject.transform.localScale = _zero;
        StageStartCanvasRect = GetComponent<RectTransform>();

        Title = transform.Find("Title").GetComponent<Image>();
        SubTitle = transform.Find("SubTitle").GetComponent<Image>();
    }
}
