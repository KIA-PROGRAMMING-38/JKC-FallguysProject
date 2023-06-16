using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class LoadingSceneSplashArtCardPooler : MonoBehaviour
{
    private LoadingSceneSplashArtCardPool _cardPool;
    private HorizontalRendererView _horizontalRendererView;
    private RectTransform _respawnZone;
    
    [SerializeField] 
    private float _reapeatCooldown;

    private CancellationTokenSource _reapeatCancellationTokenSource;
    
    private void Awake()
    {
        _cardPool = new LoadingSceneSplashArtCardPool(gameObject);
        _reapeatCancellationTokenSource = new CancellationTokenSource();
        
        ReapeatHorizontalEffect(_reapeatCancellationTokenSource.Token).Forget();
    }

    private Stack<CancellationTokenSource> cardTokenStack = new Stack<CancellationTokenSource>();
    private async UniTaskVoid ReapeatHorizontalEffect(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_reapeatCooldown), cancellationToken: cancelToken);
            
            CancellationTokenSource cardCancellationTokenSource = new CancellationTokenSource();
            
            LoadingSceneSplashArtCard card = _cardPool.CardPoolInstance.Get();
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardRect.offsetMin = new Vector2(2200, 320); // left, bottom
            cardRect.offsetMax = new Vector2(880, -380); // -right, -top

            int randomSpriteIndex = Random.Range(0, SplashArtRegistry.SpriteArts.Count);
            card.SplashImage.sprite = SplashArtRegistry.SpriteArts[randomSpriteIndex];
            
            // 생성된 카드에 대해 ReleaseCard 작업을 시작하고 취소 토큰을 저장합니다.
            ReleaseCard(card, cardCancellationTokenSource.Token).Forget();
            cardTokenStack.Push(cardCancellationTokenSource);
        }
    }

    private async UniTaskVoid ReleaseCard(LoadingSceneSplashArtCard card, CancellationToken cancelToken)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: cancelToken);
        
        card.Release();
    }
    
    private void OnDestroy()
    {
        _reapeatCancellationTokenSource.Cancel();
        
        // 모든 카드에 대한 작업을 취소합니다.
        while (cardTokenStack.Count > 0)
        {
            CancellationTokenSource cardCancellationTokenSource = cardTokenStack.Pop();
            cardCancellationTokenSource.Cancel();
        }
    }
}
