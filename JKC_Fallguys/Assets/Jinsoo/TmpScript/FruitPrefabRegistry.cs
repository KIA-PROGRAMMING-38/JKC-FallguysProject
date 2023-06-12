using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

/// <summary>
/// FruitChute맵이 로드될 때, 과일들의 데이터가 저장 될 정적 클래스입니다.
/// </summary>
public static class FruitPrefabRegistry
{
    public static Dictionary<int, Fruit> Repository = new Dictionary<int, Fruit>();

    public static readonly int Apple = 0;
    public static readonly int Banana = 1;
    public static readonly int Orange = 2;
    public static readonly int StrawBerry = 3;
    public static readonly int WaterMelon = 4;

    static FruitPrefabRegistry()
    {
        GetFruitsData();
    }
    
    // 과일 데이터를 가져오는 메서드입니다.
    public static void GetFruitsData()
    {
        string[] fruits = new string[] { "Apple", "Banana", "Orange", "Strawberry", "Watermelon" };
        
        for (int i = 0; i < fruits.Length; i++)
        {
            string fruitPath = DataManager.SetDataPath(PathLiteral.Prefabs, "FruitChute", "Fruits", fruits[i]);
            Fruit fruit = Resources.Load<Fruit>(fruitPath);

            if (!Repository.ContainsKey(i))
            {
                Repository.Add(i, fruit);
            }
        }
    }
}
