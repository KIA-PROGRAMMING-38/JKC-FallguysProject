using UnityEngine;

public class ReleaseZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("LineEffect"))
        {
            LineEffect lineEffect = col.GetComponent<LineEffect>();
            lineEffect.Release();
        }
    }
}