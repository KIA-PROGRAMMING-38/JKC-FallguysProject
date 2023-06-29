using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

public static class PlayerTextureRegistry
{
    public static List<Texture> PlayerTextures;

    static PlayerTextureRegistry()
    {
        GetPlayerTextures();
    }

    private static void GetPlayerTextures()
    {
        PlayerTextures = new List<Texture>(
            Resources.LoadAll<Texture>(
                DataManager.SetDataPath(PathLiteral.Textures, PathLiteral.PlayerTexture)));
    }
}