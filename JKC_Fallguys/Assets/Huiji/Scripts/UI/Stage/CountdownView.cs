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

    // private IEnumerator Start()
    // {
    //     yield return _delay;
    //     
    //     CountdownImage.gameObject.SetActive(true);
    // }

    
}
