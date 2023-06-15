using UnityEngine;

public class CanonController : MonoBehaviour
{
    private Canon _canon;

    public FruitPooler FruitPooler;
    private void Awake()
    {
        _canon = transform.Find("CanonBody").GetComponent<Canon>();
        Debug.Assert(_canon != null);
    }

    private void Start()
    {
        _canon.Initialize(FruitPooler);
    }

    public void Initialize(FruitPooler fruitPooler)
    {
        FruitPooler = fruitPooler;
    }
}
