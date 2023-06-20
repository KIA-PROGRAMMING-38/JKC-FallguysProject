using System;
using UnityEngine;

public class HowToPlayViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("HowToPlayView").GetComponent<HowToPlayView>();
        Debug.Assert(View != null);
        Presenter = new HowToPlayPresenter();
        Debug.Assert(Presenter != null);
    }
}
