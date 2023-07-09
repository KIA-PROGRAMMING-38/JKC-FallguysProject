using Newtonsoft.Json;
using UnityEngine;

public static class ResourceManager
{
   public static GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        return Instantiate(prefab, parent);
    }
    
    private static GameObject Instantiate(GameObject prefab, Transform parent = null)
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