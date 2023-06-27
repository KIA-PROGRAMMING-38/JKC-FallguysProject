using System;
using LiteralRepository;
using Model;
using UnityEngine;

public class ResultRoundSetupManager : MonoBehaviour
{
    // 프리팹 불러오기
    private GameObject[] _fallGuy = new GameObject[3];
    
    // 생성될 위치랑 애니메이터 배열로 선언하기
    private Vector3[] _fallGuyPositions = new[]
    {
        new Vector3(-3.17f, 2.25f, 0f),
        new Vector3(3.96f, 0.25f, 0f),
        new Vector3(11.185f, -2.04f, 0f)
    };

    private RuntimeAnimatorController[] _runtimeAnimator;

    private Animator _animator;
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _runtimeAnimator = Resources.LoadAll<RuntimeAnimatorController>(DataManager.SetDataPath(PathLiteral.AnimatorController, "ResultRoundAnimator"));
    }

    private void Start()
    {
        SetPlayerPrefab();
    }

    /// <summary>
    /// 1,2,3위에 해당하는 플레이어의 프리팹을 생성합니다.
    /// </summary>
    private void SetPlayerPrefab()
    {
        for (int fallGuyIndex = 0; fallGuyIndex < _fallGuy.Length; ++fallGuyIndex)
        {
            _fallGuy[fallGuyIndex] = DataManager.GetGameObjectData(
                DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, "05_RoundResult", "RoundResultFallGuy"));

            GameObject fallGuy = Instantiate(_fallGuy[fallGuyIndex], _fallGuyPositions[fallGuyIndex],
                _fallGuy[fallGuyIndex].transform.rotation);

            _animator = fallGuy.GetComponent<Animator>();
            _meshRenderer = fallGuy.GetComponent<MeshRenderer>();
            Texture texture = PlayerTextureRegistry.PlayerTextures[RoundResultSceneModel.FallguyRankings[fallGuyIndex].TextureIndex]; 
            _meshRenderer.material.mainTexture = texture;
            _animator.runtimeAnimatorController = _runtimeAnimator[fallGuyIndex];
        }
    }
}
