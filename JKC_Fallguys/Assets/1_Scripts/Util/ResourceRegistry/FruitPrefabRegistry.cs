using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

/// <summary>
/// FruitChute맵이 로드될 때, 과일들의 데이터가 저장 될 정적 클래스입니다.
/// </summary>
public static class FruitPrefabRegistry
{
    public static List<Fruit> Repository = new List<Fruit>();
    public static List<string> PathRepository = new List<string>();

    static FruitPrefabRegistry()
    {
        GetFruitsData();
        GetFruitFilePath();
    }

    public static void GetFruitFilePath()
    {
        string[] fruits = new string[] { "Apple", "Banana", "Orange", "Strawberry", "Watermelon" };
        
        for (int i = 0; i < fruits.Length; i++)
        {
            string fruitPath = ResourceManager.SetDataPath(PathLiteral.Prefabs, "Stage", "FruitChute", "Fruits", fruits[i]);
            
            PathRepository.Add(fruitPath);
        }
    }
    
    // 과일 데이터를 가져오는 메서드입니다.
    public static void GetFruitsData()
    {
        string[] fruits = new string[] { "Apple", "Banana", "Orange", "Strawberry", "Watermelon" };
        
        for (int i = 0; i < fruits.Length; i++)
        {
            string fruitPath = ResourceManager.SetDataPath(PathLiteral.Prefabs, "Stage", "FruitChute", "Fruits", fruits[i]);
            Fruit fruit = Resources.Load<Fruit>(fruitPath);
            
            Repository.Add(fruit);
        }
    }
}
