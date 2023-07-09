using System.Collections.Generic;
using System.IO;
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
            (Resources.LoadAll<Sprite>(Path.Combine(PathLiteral.Sprites, PathLiteral.UI, PathLiteral.GameLoading, "SplashArts")));
    }
}