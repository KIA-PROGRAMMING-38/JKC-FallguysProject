using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CostumeView : View
{
    public Image Highlight { get; private set; }
    public Button SelectButton { get; private set; }

    private void Awake()
    {
        Highlight = transform.Find("LayoutGroup").Find("Highlight").GetComponent<Image>();
        SelectButton = EventSystem.current.firstSelectedGameObject.GetComponent<Button>();
    }

    // private void Update()
    // {
    //     SelectButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
    // }
}
