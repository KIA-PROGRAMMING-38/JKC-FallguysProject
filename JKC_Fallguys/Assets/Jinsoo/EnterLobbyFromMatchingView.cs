using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class EnterLobbyFromMatchingView : View
{
    public Button UIPopUpButton { get; private set; }

    private void Awake()
    {
        UIPopUpButton = transform.Find("UIPopUpButton").GetComponent<Button>();
    }
}
