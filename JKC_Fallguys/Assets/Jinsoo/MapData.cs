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
        public string FilePath;
        public string MapName;
        public string Description;
        public string Purpose;
    }

    public MapType Type;
    public Vector3[] PlayerSpawnPosition;
    public string SplashArt;
    public MapInfo Info;
}
