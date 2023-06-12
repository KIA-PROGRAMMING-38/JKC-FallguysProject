using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

public static class CountdownSpriteRegistry
{
    //private static Sprite[] _sprites = Resources.LoadAll<Sprite>(DataManager.SetDataPath(PathLiteral.UI, PathLiteral.Stage, PathLiteral.Countdown));

    private static List<Sprite> _sprites;
    public static List<Sprite> Sprites => _sprites;

    static CountdownSpriteRegistry()
    {
        Init();   
    }

    private static void Init()
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(DataManager.SetDataPath(PathLiteral.UI, PathLiteral.Stage, PathLiteral.Countdown));
        _sprites = new List<Sprite>(loadedSprites);
    }
}