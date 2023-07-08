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

    public static T Load<T>(string filePath) where T : Object
    {
        T resource = Resources.Load<T>(filePath);

        if (resource == null)
        {
            Debug.LogError("Resource not found at path: " + filePath);
        }

        return resource;
    }

    public static T[] LoadAll<T>(string filePath) where T : Object
    {
        T[] resources = Resources.LoadAll<T>(filePath);
        
        if (resources == null)
        {
            Debug.LogError("Resource not found at path: " + filePath);
        }

        return resources;
    }
    
    public static GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/Scene/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        return Instantiate(prefab, parent);
    }
    
    public static GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
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