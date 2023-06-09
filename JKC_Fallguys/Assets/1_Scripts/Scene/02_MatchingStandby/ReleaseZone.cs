using UnityEngine;
using LiteralRepository;

public class ReleaseZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.LineEffect))
        {
            LineEffect lineEffect = col.GetComponent<LineEffect>();
            lineEffect.Release();
        }
    }
}