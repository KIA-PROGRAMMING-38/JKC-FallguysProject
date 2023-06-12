using UnityEngine;

public class FruitPooler : MonoBehaviour
{
    private FruitPool _fruitPool;
    public FruitPool FruitPool
    {
        get { return _fruitPool; }
    }
    
    private void Awake()
    {
        _fruitPool = new FruitPool(gameObject);
    }
}
