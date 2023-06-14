using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

public static class SplashArtRegistry
{
    public static Queue<Sprite> SpriteArts;

    static SplashArtRegistry()
    {
        GetSplashArtData();
    }

    public static void GetSplashArtData()
    {
        SpriteArts = new Queue<Sprite>
            (Resources.LoadAll<Sprite>(DataManager.SetDataPath(PathLiteral.UI, "GameLoading")));
    }
}