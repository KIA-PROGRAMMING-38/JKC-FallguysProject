using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundResultViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RoundResultView").GetComponent<RoundResultView>();
        Debug.Assert(View != null);
        Presenter = new RoundResultPresenter();
        Debug.Assert(Presenter != null);
    }
}
