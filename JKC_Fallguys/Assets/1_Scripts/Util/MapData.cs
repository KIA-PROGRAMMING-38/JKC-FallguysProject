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
        public MapType Type;
        public string MapName;
        public int SplashArtRegistryIndex;
        public string Description;
        public string Purpose;
    }

    public struct MapInstanceData
    {
        public Vector3[] PlayerSpawnPosition;
        public Vector3 MapPosition;
        public Quaternion MapRotation;
        public string PrefabFilePath;
    }
    
    public MapInfo Info;
    public MapInstanceData Data;
}
