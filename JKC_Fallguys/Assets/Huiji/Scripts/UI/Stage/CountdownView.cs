using UnityEngine;
using UnityEngine.UI;

public class CountdownView : View
{
    public Image CountdownImage { get; private set; }
    private Vector3 _zero = Vector3.zero;

    private void Awake()
    {
        CountdownImage = transform.Find("CountdownImage").GetComponent<Image>();
        CountdownImage.gameObject.transform.localScale = _zero;
    }
}
