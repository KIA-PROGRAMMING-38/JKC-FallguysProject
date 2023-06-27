using UnityEngine;

public class MapData
{
    public enum MapType
    {
        Default,
        Race,
        Survivor,
        Hurt
    }

    public struct MapInfo
    {
        public string MapName;
        public int SplashArtRegistryIndex;
        public string Description;
        public string Purpose;
    }

    public MapType Type;
    public MapInfo Info;
    public Vector3[] PlayerSpawnPosition;
    public string PrefabFilePath;
}
