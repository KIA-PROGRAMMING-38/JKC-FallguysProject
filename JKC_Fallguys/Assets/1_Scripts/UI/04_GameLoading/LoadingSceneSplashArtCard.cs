using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneSplashArtCard : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // 스플래시 카드의 이미지를 계속해서 변환하기 위한 객체.
    public Image SplashImage;
    public LoadingSceneSplashArtCardPool PoolOwner { private get; set; }

    private RectTransform _rectTransform;
    private RectTransform _parentCanvasRect;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _parentCanvasRect = GetComponentInParent<RectTransform>();
    }

    private Vector3 _spawnPos = new Vector3(1600, -30, 0);
    private void OnEnable()
    {
        _rectTransform.anchoredPosition = _spawnPos;
        MoveToLeft();
    }
    
    private Vector2 _cardTargetPos = new Vector2(-1, 0.5f);
    private void MoveToLeft()
    {
        _rectTransform.MoveUIAtSpeed(_cardTargetPos, _parentCanvasRect, moveSpeed);
    }
    
    public void SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    public void Release()
    {
        PoolOwner.CardPoolInstance.Release(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
