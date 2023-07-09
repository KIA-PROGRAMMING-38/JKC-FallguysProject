using System.IO;
using LiteralRepository;
using Model;
using ResourceRegistry;
using UnityEngine;

public class RoundResultSetupManager : MonoBehaviour
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
    private SkinnedMeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _runtimeAnimator = Resources.LoadAll<RuntimeAnimatorController>
            (Path.Combine("Animation", "Controller", "ResultRoundAnimator"));
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
            if (fallGuyIndex >= RoundResultSceneModel.FallguyRankings.Count)
                break;
            
            _fallGuy[fallGuyIndex] = ResourceManager.Load<GameObject>(
                Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.RoundResult, "RoundResultFallGuy"));

            GameObject fallGuy = 
                Instantiate(_fallGuy[fallGuyIndex], _fallGuyPositions[fallGuyIndex], _fallGuy[fallGuyIndex].transform.rotation);

            SetFallGuysAnimation(fallGuy, fallGuyIndex);

            SetFallGuysTexture(fallGuy, fallGuyIndex);
        }
    }

    private void SetFallGuysAnimation(GameObject fallGuy, int fallGuyIndex)
    {
        _animator = fallGuy.GetComponent<Animator>();
        _animator.runtimeAnimatorController = _runtimeAnimator[fallGuyIndex];
    }

    private void SetFallGuysTexture(GameObject fallGuy, int fallGuyIndex)
    {
        _meshRenderer = fallGuy.GetComponentInChildren<SkinnedMeshRenderer>();
        Texture texture = PlayerTextureRegistry.PlayerTextures[RoundResultSceneModel.FallguyRankings[fallGuyIndex].TextureIndex]; 
        _meshRenderer.material.mainTexture = texture;
    }
}
