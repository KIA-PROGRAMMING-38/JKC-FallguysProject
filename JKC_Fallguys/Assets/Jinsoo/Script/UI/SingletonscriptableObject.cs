using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonscriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // 모든 타입의 ScriptableObject를 찾는다
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                
                // 찾아낸 결과가 없다면, 로그에 에러를 출력하고 null 반환
                // 이는 T 타입의 ScriptableObject가 존재하지 않음을 의미
                if (results.Length == 0)
                {
                    Debug.LogError("SingletonScriptableObject => Instance => results length is 0 for type " + typeof(T).ToString() + ".");
                    return null;
                }

                // 찾아낸 결과가 1개를 초과한다면, 로그에 에러를 출력하고 null 반환
                // 이는 T 타입의 ScriptableObject가 여러 개 있음을 의미하는데 Singleton 원칙을 위반
                if (results.Length > 1)
                {
                    Debug.LogError("SingletonScriptableObject => Instance => results length is greather han 1 for type " + typeof(T).ToString() + ".");
                    return null;
                }

                // 결과 배열의 첫 번째 요소를 _instance에 할당
                _instance = results[0];
            }

            // 찾아진 ScriptableObject 타입의 객체가 한 개 일때 인스턴스가 정상적으로 할당
            return _instance;
        }
        
    }
    
}
