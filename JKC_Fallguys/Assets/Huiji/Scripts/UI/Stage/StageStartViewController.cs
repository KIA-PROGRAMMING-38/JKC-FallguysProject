using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStartViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("StageStartView").GetComponent<StageStartView>();
        Debug.Assert(View != null);
        Presenter = new StageStartPresenter();
        Debug.Assert(Presenter != null);
    }
}
