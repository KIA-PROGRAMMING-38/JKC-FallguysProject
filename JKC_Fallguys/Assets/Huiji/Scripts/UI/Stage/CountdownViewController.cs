using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("CountdownView").GetComponent<CountdownView>();
        Debug.Assert(View != null);
        Presenter = new CountdownPresenter();
        Debug.Assert(Presenter != null);
    }
}
