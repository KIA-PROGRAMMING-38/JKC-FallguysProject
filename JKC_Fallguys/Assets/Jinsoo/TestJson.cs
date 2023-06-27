using System;
using System.Collections;
using System.Collections.Generic;
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
        TextAsset mapJson = Resources.Load<TextAsset>(filePath);
        if (mapJson == null)
        {
            Debug.LogError($"Failed to load map JSON file at path: {filePath}");
            return;
        }

        MapData mapData = JsonUtility.FromJson<MapData>(mapJson.text);
        StageDataManager.Instance.MapDatas.Add(mapId, mapData);

        Debug.Log(mapData);
    }
}
