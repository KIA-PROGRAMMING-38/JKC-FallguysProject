using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : View
{
    public Image ResultTextImage { get; private set; }

    private WaitForSeconds _delay = new WaitForSeconds(2f);
    private void Awake()
    {
        ResultTextImage = transform.Find("ResultTextImage").GetComponent<Image>();
        ResultTextImage.gameObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        yield return _delay;
        
        ResultTextImage.gameObject.SetActive(true);
    }
}
