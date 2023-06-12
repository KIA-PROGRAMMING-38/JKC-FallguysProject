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
        
    }

    private int _spriteIndex;
    private void SetSprite()
    {
        if (_spriteIndex < CountdownSpriteRegistry.Sprites.Count)
        {
            
        }
    }

    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        
    }
}
