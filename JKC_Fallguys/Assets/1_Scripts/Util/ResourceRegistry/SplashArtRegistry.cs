using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

public static class SplashArtRegistry
{
    public static List<Sprite> SpriteArts;

    static SplashArtRegistry()
    {
        GetSplashArtData();
    }

    public static void GetSplashArtData()
    {
        SpriteArts = new List<Sprite>
            (Resources.LoadAll<Sprite>(DataManager.SetDataPath(PathLiteral.UI, PathLiteral.GameLoading)));
    }
}