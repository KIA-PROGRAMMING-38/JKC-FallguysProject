using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownView : View
{
    public Image CountdownImage { get; private set; }
    
    private WaitForSeconds _delay = new WaitForSeconds(1f);

    private void Awake()
    {
        CountdownImage = transform.Find("CountdownImage").GetComponent<Image>();
        CountdownImage.gameObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        yield return _delay;
        
        CountdownImage.gameObject.SetActive(true);

        CountdownImage.transform.localScale = Vector3.zero;
        
        Scale();
    }

    private void Scale()
    {
        float targetScale = 1.0f;
        float duration = 1.5f;

        CountdownImage.transform.ScaleTween(Vector3.one * targetScale, duration)
            .SetEase(Ease.EaseOutElastic)
            .SetDelay(0.2f);            
    }
}
