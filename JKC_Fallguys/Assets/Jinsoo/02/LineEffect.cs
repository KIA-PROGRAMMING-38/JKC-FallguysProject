using UnityEngine;

public class LineEffect : MonoBehaviour
{
    // 이 LineEffect가 속한 풀의 소유자입니다.
    public LineEffectPool PoolOwner { private get; set; }

    private float _moveSpeed = 12f;  

    /// <summary>
    /// LineEffect 객체는 생성되면 Vector3.up 방향으로 이동합니다
    /// </summary>
    public void Update()
    {
        transform.Translate(Vector3.up * (_moveSpeed * Time.deltaTime));
    }

    public void SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    public void Release()
    {
        PoolOwner.LineEffectPoolInstance.Release(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
