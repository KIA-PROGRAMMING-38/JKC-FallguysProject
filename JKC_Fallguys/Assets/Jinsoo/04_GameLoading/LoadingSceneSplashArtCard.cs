using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneSplashArtCard : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // 스플래시 카드의 이미지를 계속해서 변환하기 위한 객체.
    public Image SplashImage;
    public LoadingSceneSplashArtCardPool PoolOwner { private get; set; }
    public CancellationTokenSource ReleaseCancellationTokenSource { get; private set; }
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        ReleaseCancellationTokenSource = new CancellationTokenSource();
    }

    private void Update()
    {
        MoveToLeft();
    }

    private void MoveToLeft()
    {
        _rectTransform.anchoredPosition -= new Vector2(moveSpeed, 0f);
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

    private void OnDestroy()
    {
        ReleaseCancellationTokenSource.Cancel();
    }
}
