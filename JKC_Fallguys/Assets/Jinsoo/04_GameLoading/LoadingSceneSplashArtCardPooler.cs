using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
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

    private async UniTaskVoid ReapeatHorizontalEffect(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_reapeatCooldown), cancellationToken: cancelToken);
            
            LoadingSceneSplashArtCard card = _cardPool.CardPoolInstance.Get();
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardRect.offsetMin = new Vector2(2200, 320); // left, bottom
            cardRect.offsetMax = new Vector2(880, -380); // -right, -top

            int randomSpriteIndex = Random.Range(0, SplashArtRegistry.SpriteArts.Count);
            card.SplashImage.sprite = SplashArtRegistry.SpriteArts[randomSpriteIndex];
            
            ReleaseCard(card, card.ReleaseCancellationTokenSource.Token).Forget();
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
    }
}
