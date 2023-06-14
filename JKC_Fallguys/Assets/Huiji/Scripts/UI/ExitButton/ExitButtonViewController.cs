using System;
using UnityEngine;

public class ExitButtonViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("ExitButtonView").GetComponent<ExitButtonView>();
        Debug.Assert(View != null);
        Presenter = new ExitButtonPresenter();
        Debug.Assert(Presenter != null);
    }
}
