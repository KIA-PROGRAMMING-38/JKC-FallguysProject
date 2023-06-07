using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if ( _instance == null )
            {
                _instance = FindObjectOfType<T>();
                if ( _instance == null )
                {
                    GameObject obj = new GameObject( $"@{nameof( T )}" );
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad( obj );
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if ( _instance != null )
        {
            if ( _instance != this )
            {
                Destroy( gameObject );
            }
            return;
        }

        _instance = GetComponent<T>();
        DontDestroyOnLoad( _instance );
    }
}
