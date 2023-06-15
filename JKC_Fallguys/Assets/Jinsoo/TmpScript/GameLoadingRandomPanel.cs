using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLoadingRandomPanel : MonoBehaviour
{
    private Image _spriteImage;

    [SerializeField] 
    private float _changeInterval;

    private void Awake()
    {
        _spriteImage = transform.Find("SpriteImage").GetComponent<Image>();
        Debug.Assert(_spriteImage != null);
        
        SetRandomSprite().Forget();
    }

    private async UniTaskVoid SetRandomSprite()
    {
        while (true)
        {
            int randomSpriteIndex = Random.Range(0, SplashArtRegistry.SpriteArts.Count);
            _spriteImage.sprite = SplashArtRegistry.SpriteArts[randomSpriteIndex];
            
            await UniTask.Delay(TimeSpan.FromSeconds(_changeInterval));
        }
    }
}
