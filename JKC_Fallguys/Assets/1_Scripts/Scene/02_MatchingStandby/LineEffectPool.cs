using System.Collections.Generic;
using System.IO;
using LiteralRepository;
using UnityEngine;
using UnityEngine.Pool;

public class LineEffectPool
{
    // Object Pool 인스턴스를 관리하는 프로퍼티.
    public ObjectPool<LineEffect> LineEffectPoolInstance { get; private set; }
    
    private Queue<LineEffect> _lineEffectPrefabs;
    // LineEffect 인스턴스의 부모 객체.
    private GameObject _parentObject;
    
    /// <summary>
    /// 생성자에서 부모 객체를 받아 LineEffectPool을 초기화합니다.
    /// </summary>
    public LineEffectPool(GameObject parent)
    {
        _lineEffectPrefabs = SetLineEffectPrefabs();
        _parentObject = parent;
        
        LineEffectPoolInstance = new ObjectPool<LineEffect>
            (CreateLineEffect, ActionOnGet, ActionOnRelease, ActionOnDestroy,
            true, 28, 40);
    }

    // LineEffect 프리팹을 불러와 큐에 저장합니다
    private Queue<LineEffect> SetLineEffectPrefabs()
    {
        string filePath = Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, "LineEffect");

        LineEffect[] lineEffects = Resources.LoadAll<LineEffect>(filePath);
        Queue<LineEffect> lineEffectsList = new Queue<LineEffect>();

        for (int i = 0; i < 2; ++i)
        {
            foreach (LineEffect elem in lineEffects)
            {
                lineEffectsList.Enqueue(elem);
            }
        }
        
        return lineEffectsList;
    }

    private LineEffect CreateLineEffect()
    {
        LineEffect lineEffectPrefab = _lineEffectPrefabs.Dequeue();
        LineEffect lineEffect = GameObject.Instantiate(lineEffectPrefab, _parentObject.transform);
        lineEffect.PoolOwner = this;

        return lineEffect;
    }

    private void ActionOnGet(LineEffect lineEffect)
    {
        lineEffect.SetActive(true);
    }

    private void ActionOnRelease(LineEffect lineEffect)
    {
        lineEffect.SetActive(false);
    }

    private void ActionOnDestroy(LineEffect lineEffect)
    {
        lineEffect.Destroy();
    }
}