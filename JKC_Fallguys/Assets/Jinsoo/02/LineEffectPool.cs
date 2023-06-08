using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;
using UnityEngine.Pool;

public class LineEffectPool
{
    public ObjectPool<LineEffect> LineEffectPoolInstance { get; private set; }
    private Queue<LineEffect> _lineEffectPrefabs;
    private GameObject _parentObject;
    
    public LineEffectPool(GameObject parent)
    {
        _lineEffectPrefabs = SetLineEffectPrefabs();
        _parentObject = parent;
        
        LineEffectPoolInstance = new ObjectPool<LineEffect>
            (CreateLineEffect, ActionOnGet, ActionOnRelease, ActionOnDestroy,
            true, 28, 40);
    }

    private Queue<LineEffect> SetLineEffectPrefabs()
    {
        string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, "LineEffect");

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