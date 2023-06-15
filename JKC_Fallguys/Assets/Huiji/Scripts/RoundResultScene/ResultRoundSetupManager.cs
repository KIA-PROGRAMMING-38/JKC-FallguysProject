using System;
using LiteralRepository;
using Model;
using UnityEngine;

public class ResultRoundSetupManager : MonoBehaviour
{
    // 프리팹 불러오기
    private GameObject[] _fallGuy = new GameObject[3];
    
    // 큐에 골에 들어간 플레이어 인원담기
    private void Awake()
    {
        for (int fallGuyIndex = 0; fallGuyIndex < _fallGuy.Length; ++fallGuyIndex)
        {
            _fallGuy[fallGuyIndex] = DataManager.GetGameObjectData(
                DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, "05_RoundResult", "RoundResultFallGuy")); 
        }
    }

    
    private void Start()
    {
        
    }
    
    // 생성될 위치랑 애니메이터 배열로 선언하기
    // 반복문을 돌면서 프리팹 생성하기
    // 생성할때 위치랑 애니메이터 할당하기
}
