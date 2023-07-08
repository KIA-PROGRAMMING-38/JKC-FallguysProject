using System.Collections.Generic;
using System.IO;
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
            Resources.LoadAll<Texture>(Path.Combine(PathLiteral.Sprites, PathLiteral.PlayerTexture)));
    }
}