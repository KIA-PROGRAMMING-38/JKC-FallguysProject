using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CostumeButtonAction : MonoBehaviour, ISelectHandler, IPointerClickHandler, ISubmitHandler
{
    private Image _highlight;
    private Vector2 _senterVec = new Vector2(0.5f, 0.5f);
    private Vector2 _zeroVec = Vector3.zero;
    private void Awake()
    {
        _highlight = transform.parent.Find("Highlight").GetComponent<Image>();
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
        Debug.Log("버튼 클릭!");
    }
}
