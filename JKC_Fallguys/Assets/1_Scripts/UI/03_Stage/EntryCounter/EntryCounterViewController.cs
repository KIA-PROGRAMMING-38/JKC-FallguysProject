using System;
using UnityEngine;

public class EntryCounterViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("EntryCounterView").GetComponent<EntryCounterView>();
        Debug.Assert(View != null);
        Presenter = new EntryCounterPresenter();
        Debug.Assert(Presenter != null);
    }
}
