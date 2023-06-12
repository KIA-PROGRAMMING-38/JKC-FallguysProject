using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownPresenter : Presenter
{
    private CountdownView _countdownView;
    public override void OnInitialize(View view)
    {
        _countdownView = view as CountdownView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        SetSprite();
    }

    private int _spriteIndex;
    private void SetSprite()
    {
        if (_spriteIndex < CountdownSpritesRegistry.Sprites.Count)
        {
            Sprite sprite = CountdownSpritesRegistry.Sprites[_spriteIndex];

            _countdownView.CountdownImage.sprite = sprite;
        }
    }

    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        
    }
}
