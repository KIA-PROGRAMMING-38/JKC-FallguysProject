using System;
using LiteralRepository;
using UnityEngine;

public class ResultRoundSetupManager : MonoBehaviour
{
    // 프리팹 불러오기
    private GameObject _fallGuy;

    private void Awake()
    {
        _fallGuy = DataManager.GetGameObjectData(DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene,
            "05_RoundResult", "RoundResultFallGuy"));
        
        Debug.Log(_fallGuy);
    }
    // 큐에 골에 들어간 플레이어 인원담기
    // 생성될 위치랑 애니메이터 배열로 선언하기
    // 반복문을 돌면서 프리팹 생성하기
    // 생성할때 위치랑 애니메이터 할당하기
}
