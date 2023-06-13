using UnityEngine;
using UnityEngine.Pool;

public class FruitPool
{
    // Object Pool 인스턴스를 관리하는 프로퍼티.
    public ObjectPool<Fruit> FruitPoolInstance { get; private set; }
    
    // Fruit 인스턴스의 부모 객체.
    private GameObject _parentObject; 

    /// <summary>
    /// 생성자에서 부모 객체를 받아 FruitPool을 초기화합니다.
    /// </summary>
    /// <param name="parent"></param>
    public FruitPool(GameObject parent)
    {
        _parentObject = parent;
        
        FruitPoolInstance = new ObjectPool<Fruit>
        (CreateFruit, ActionOnGet, ActionOnRelease, ActionOnDestroy,
            true, 50, 100);
    }

    private Fruit CreateFruit()
    {
        Fruit fruitPrefab = FruitSelection();
        Fruit fruit = GameObject.Instantiate(fruitPrefab, _parentObject.transform);
        fruit.PoolOwner = this;

        return fruit;
    }
    
    // 무작위로 생성할 과일을 선택하는 메서드입니다.
    private Fruit FruitSelection()
    {
        Fruit fruit = default;
        int randomFruitPickNumber = Random.Range(0, 5);
        fruit = FruitPrefabRegistry.Repository[randomFruitPickNumber];

        return fruit;
    }

    private void ActionOnGet(Fruit fruit)
    {
        fruit.SetActive(true);
    }

    private void ActionOnRelease(Fruit fruit)
    {
        fruit.SetActive(false);
    }

    private void ActionOnDestroy(Fruit fruit)
    {
        fruit.Destroy();
    }
}
