using Newtonsoft.Json;
using UniRx;
using UnityEngine;

public static class ResourceManager
{
    public static readonly int MaxPlayableMaps = 3;

    private static ReactiveProperty<int> _playerTextureIndex = new ReactiveProperty<int>();
    public static IReactiveProperty<int> PlayerTextureIndex = _playerTextureIndex;

    public static void SetPlayerTexture(int index)
    {
        _playerTextureIndex.Value = index;
    }

    public static T Load<T>(params string[] filePath) where T : Object
    {
        T resource = Resources.Load<T>(SetDataPath(filePath));

        if (resource == null)
        {
            Debug.LogError("Resource not found at path: " + SetDataPath(filePath));
        }

        return resource;
    }

    public static T[] LoadAll<T>(params string[] filePath) where T : Object
    {
        T[] resources = Resources.LoadAll<T>(SetDataPath(filePath));
        
        if (resources == null)
        {
            Debug.LogError("Resource not found at path: " + SetDataPath(filePath));
        }

        return resources;
    }
    
    /// <summary>
    /// 데이터 바인딩 위한 경로를 설정합니다.
    /// </summary>
    /// <param name="filePath">파일 경로 세그먼트를 나타내는 문자열 배열입니다.</param>
    /// <returns>조합된 파일 경로를 나타내는 문자열을 반환합니다.</returns>
    public static string SetDataPath(params string[] filePath)
    {
        return string.Join("/", filePath);
    }

    public static T JsonLoader<T>(string filePath)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);

        if (textAsset == null)
        {
            Debug.LogError($"Failed to load JSON file at path: {filePath}");
            return default(T);
        }

        string jsonContent = textAsset.text;
        T data = JsonConvert.DeserializeObject<T>(jsonContent);

        return data;
    }

}