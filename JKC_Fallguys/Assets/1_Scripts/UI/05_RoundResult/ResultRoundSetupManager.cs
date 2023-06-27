using LiteralRepository;
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
    
    private void Awake()
    {
        _runtimeAnimator = Resources.LoadAll<RuntimeAnimatorController>(DataManager.SetDataPath(PathLiteral.AnimatorController, "ResultRoundAnimator"));
        
        // 반복문을 돌면서 프리팹 생성하기
        for (int fallGuyIndex = 0; fallGuyIndex < _fallGuy.Length; ++fallGuyIndex)
        {
            _fallGuy[fallGuyIndex] = DataManager.GetGameObjectData(
                DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, "05_RoundResult", "RoundResultFallGuy"));

            GameObject fallGuy = Instantiate(_fallGuy[fallGuyIndex], _fallGuyPositions[fallGuyIndex],
                _fallGuy[fallGuyIndex].transform.rotation);

            _animator = fallGuy.GetComponent<Animator>();
            _animator.runtimeAnimatorController = _runtimeAnimator[fallGuyIndex];
        }
    }
}
