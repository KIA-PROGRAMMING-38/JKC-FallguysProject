using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class LoadingSceneSplashArtCardPooler : MonoBehaviour
{
    private LoadingSceneSplashArtCardPool _cardPool;
    
    [SerializeField] 
    private float _repeatCooldown;

    private CancellationTokenSource _repeatEffectCancellationTokenSource;
    private CancellationTokenSource _releaseCardCancellationTokenSource;
    
    private void Awake()
    {
        _cardPool = new LoadingSceneSplashArtCardPool(gameObject);
        _repeatEffectCancellationTokenSource = new CancellationTokenSource();
        _releaseCardCancellationTokenSource = new CancellationTokenSource();
        
        RepeatHorizontalEffect(_repeatEffectCancellationTokenSource.Token).Forget();
    }

    private async UniTaskVoid RepeatHorizontalEffect(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_repeatCooldown), cancellationToken: cancelToken);
        
            LoadingSceneSplashArtCard card = _cardPool.CardPoolInstance.Get();
            int randomSpriteIndex = Random.Range(0, SplashArtRegistry.SpriteArts.Count);
            card.SplashImage.sprite = SplashArtRegistry.SpriteArts[randomSpriteIndex];
        
            // 생성된 카드에 대해 ReleaseCard 작업을 시작합니다.
            // 이 때 모든 작업은 동일한 취소 토큰을 공유합니다.
            ReleaseCard(card, _releaseCardCancellationTokenSource.Token).Forget();
        }
    }

    private async UniTaskVoid ReleaseCard(LoadingSceneSplashArtCard card, CancellationToken cancelToken)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: cancelToken);
        
        card.Release();
    }
    
    private void OnDestroy()
    {
        _repeatEffectCancellationTokenSource.Cancel();
        _releaseCardCancellationTokenSource.Cancel();
    }
}
