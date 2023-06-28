using System;
using Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CostumeButtonAction : MonoBehaviour, ISelectHandler, IPointerClickHandler, ISubmitHandler
{
    private Image _highlight;
    private Vector2 _senterVec = new Vector2(0.5f, 0.5f);
    private Vector2 _zeroVec = Vector2.zero;
    private Button _button;

    [SerializeField] private CostumeData _costumeData;
    private void Awake()
    {
        Debug.Log($"{_costumeData.CostumeName}버튼 Awake");
        _highlight = transform.parent.Find("Highlight").GetComponent<Image>();
        _button = GetComponent<Button>();
        Debug.Assert(_highlight != null);
    }

    private void OnEnable()
    {
        Debug.Log($"{_costumeData.CostumeName}버튼 OnEnable");
        if (DataManager.PlayerTextureIndex.Value == _costumeData.TextureIndex)
        {
            EventSystem.current.firstSelectedGameObject = gameObject;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _highlight.transform.SetParent(transform, false);

        _highlight.rectTransform.anchorMin = _senterVec;
        _highlight.rectTransform.anchorMax = _senterVec;

        _highlight.rectTransform.localPosition = _zeroVec; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CommonClickHandler();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        CommonClickHandler();
    }

    /// <summary>
    /// OnPointerClick, OnSubmit에서 호출해주세요.
    /// </summary>
    private void CommonClickHandler()
    {
        LobbySceneModel.SetColorName(_costumeData.CostumeName);
        DataManager.SetPlayerTexture(_costumeData.TextureIndex);
    }
}
