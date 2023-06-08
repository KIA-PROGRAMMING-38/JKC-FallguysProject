using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 리소스를 관리하고 캐싱하는 역할의 클래스입니다.
/// </summary>
public static class ResourceCache
{
    public static readonly Dictionary<string, Object> _resources = new Dictionary<string, Object>();
    
    public static T GetResource<T>(string path) where T : Object
    {
        // 만약 리소스가 존재하지 않을 시 obj는 null을 반환합니다.
        if(_resources.TryGetValue(path, out var obj) == false)
        {
            obj = Resources.Load<T>(path);
            _resources.Add(path, obj);
        }
        
        return obj as T;
    }
}