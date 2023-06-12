using System.Collections.Generic;

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
}
