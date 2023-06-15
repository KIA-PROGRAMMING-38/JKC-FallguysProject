using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLoadingRandomPanel : MonoBehaviour
{
    private Image _spriteImage;
    private CancellationTokenSource _cancellationTokenSource;

    [SerializeField] 
    private float _changeInterval;

    private void Awake()
    {
        _spriteImage = transform.Find("SpriteImage").GetComponent<Image>();
        Debug.Assert(_spriteImage != null);
        
        _cancellationTokenSource = new CancellationTokenSource();
        
        SetRandomSprite(_cancellationTokenSource.Token).Forget();
    }

    private async UniTaskVoid SetRandomSprite(CancellationToken cancelToken)
    {
        while (true)
        {
            int randomSpriteIndex = Random.Range(0, SplashArtRegistry.SpriteArts.Count);
            _spriteImage.sprite = SplashArtRegistry.SpriteArts[randomSpriteIndex];
            
            await UniTask.Delay(TimeSpan.FromSeconds(_changeInterval), cancellationToken: cancelToken);
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
