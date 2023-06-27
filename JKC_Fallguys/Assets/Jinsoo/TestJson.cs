using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    private void Awake()
    {
        MapInstanceLoad().Forget();
    }

    private async UniTaskVoid MapInstanceLoad()
    {
        for (int i = 0; i < 3; i++)
        {
            await LoadMapDataAsync($"JSON/MapData_{i:D2}", i);
        }
    }

    private async UniTask LoadMapDataAsync(string filePath, int mapId)
    {
        string absolutePath = Path.Combine(Application.dataPath, "Resources", filePath + ".json");

        if (!File.Exists(absolutePath))
        {
            Debug.LogError($"Failed to load map JSON file at path: {absolutePath}");
            return;
        }

        string jsonContent;
        using (StreamReader reader = new StreamReader(absolutePath, Encoding.UTF8))
        {
            jsonContent = await reader.ReadToEndAsync();
        }

        MapData mapData = JsonUtility.FromJson<MapData>(jsonContent);
        StageDataManager.Instance.MapDatas.Add(mapId, mapData);
        
        Debug.Log(mapData.Info.MapName);
        Debug.Log(mapData.Info.Description);
        Debug.Log(mapData.Info.Purpose);
    }
}
