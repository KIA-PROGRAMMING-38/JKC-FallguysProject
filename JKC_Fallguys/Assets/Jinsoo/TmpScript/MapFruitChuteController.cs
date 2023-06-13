using UnityEngine;

public class MapFruitChuteController : MonoBehaviour
{
    private FruitPooler _fruitPooler;
    private CanonController _leftCanon;
    private CanonController _rightCanon;

    private void Awake()
    {
        _fruitPooler = transform.Find("FruitPooler").GetComponent<FruitPooler>();
        Debug.Assert(_fruitPooler != null);
        _leftCanon = transform.Find("Canon_Left").GetComponent<CanonController>();
        Debug.Assert(_leftCanon != null);
        _rightCanon = transform.Find("Canon_Right").GetComponent<CanonController>();
        Debug.Assert(_rightCanon != null);
        
        _leftCanon.Initialize(_fruitPooler);
        _rightCanon.Initialize(_fruitPooler);
    }
}
