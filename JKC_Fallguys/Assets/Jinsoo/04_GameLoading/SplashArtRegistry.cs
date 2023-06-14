using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

public static class SplashArtRegistry
{
    public static List<Texture2D> SpriteArts;

    static SplashArtRegistry()
    {
        GetSplashArtData();

        foreach (var elem in SpriteArts)
        {
            Debug.Log(elem.name);
        }
    }

    public static void GetSplashArtData()
    {
        SpriteArts = new List<Texture2D>
            (Resources.LoadAll<Texture2D>(DataManager.SetDataPath(PathLiteral.UI, "GameLoading")));
    }
}