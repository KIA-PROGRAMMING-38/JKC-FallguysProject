using System;
using System.Collections.Generic;
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

    private void Awake()
    {
        _cardPool = new LoadingSceneSplashArtCardPool(gameObject);
        
        ReapeatHorizontalEffect().Forget();
    }

    private async UniTaskVoid ReapeatHorizontalEffect()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_reapeatCooldown));
            
            LoadingSceneSplashArtCard card = _cardPool.CardPoolInstance.Get();
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardRect.offsetMin = new Vector2(2200, 320); // left, bottom
            cardRect.offsetMax = new Vector2(880, -380); // -right, -top

            int randomSpriteIndex = Random.Range(0, SplashArtRegistry.SpriteArts.Count);
            card.SplashImage.sprite = SplashArtRegistry.SpriteArts[randomSpriteIndex];
            ReleaseCard(card).Forget();
        }
    }

    private async UniTaskVoid ReleaseCard(LoadingSceneSplashArtCard card)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        
        card.Release();
    }
}
