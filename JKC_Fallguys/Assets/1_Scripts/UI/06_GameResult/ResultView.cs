using UnityEngine;
using UnityEngine.UI;

public class ResultView : View
{
    public Image ResultTextImage { get; private set; }

    private void Awake()
    {
        ResultTextImage = transform.Find("ResultTextImage").GetComponent<Image>();
        // ResultTextImage.gameObject.SetActive(false);
        Debug.Assert(ResultTextImage != null);
    }
}
