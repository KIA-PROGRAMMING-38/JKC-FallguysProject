using UnityEngine;

/// <summary>
/// Transform 클래스의 확장 메소드를 정의
/// </summary>
public static class Transforms
{
    /// <summary>
    /// Transform 인스턴스의 모든 자식 객체를 삭제하는 역할
    /// this Transform t 키워드는 이 메서드가 Transform 클래스에 추가되는 확장 메소드임을 나타냄 
    /// </summary>
    public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
    {
        // Transform의 모든 자식에 대해 반복한다
        foreach (Transform child in t)
        {
            // true일 경우 즉시 객체를 삭제
            if (destroyImmediately)
            {
                // 게임오브젝트를 삭제하는 Unity의 내장 함수
                MonoBehaviour.DestroyImmediate(child);
            }
            // false일 경우 프레임의 끝에서 삭제된다
            else
            {
                // 게임오브젝트를 삭제하는 Unity의 내장 함수
                MonoBehaviour.Destroy(child);
            }
        }
    }
}