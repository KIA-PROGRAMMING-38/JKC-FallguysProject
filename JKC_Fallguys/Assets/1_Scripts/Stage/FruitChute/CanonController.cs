using System.Linq;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public FruitPooler FruitPooler;
    private Canon[] _canons;

    private void Awake()
    {
        _canons = transform.Cast<Transform>()
            .Select(t => t.GetComponent<Canon>())
            .Where(c => c != null)
            .ToArray();

        Debug.Assert(_canons != null);
    }


    private void Start()
    {
        foreach (Canon elem in _canons)
        {
            elem.Initialize(FruitPooler);
        }
    }

    public void Initialize(FruitPooler fruitPooler)
    {
        FruitPooler = fruitPooler;
    }
}
