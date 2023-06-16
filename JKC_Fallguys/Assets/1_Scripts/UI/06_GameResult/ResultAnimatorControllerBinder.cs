using LiteralRepository;
using Model;
using UnityEngine;

public class ResultAnimatorControllerBinder : MonoBehaviour
{
    private Animator _animator;
    private RuntimeAnimatorController _victoryController;
    private RuntimeAnimatorController _loseController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _victoryController = DataManager.GetRuntimeAnimatorController(PathLiteral.AnimatorController, PathLiteral.VictoryAnimatorController);
        _loseController = DataManager.GetRuntimeAnimatorController(PathLiteral.AnimatorController, PathLiteral.LoseAnimatorController);
    }

    /// <summary>
    /// 승리 여부에 따라 애니메이션 컨트롤러를 바인딩합니다.
    /// </summary>
    private void Start()
    {
        if (ResultSceneModel.IsVictorious.Value)
        {
            _animator.runtimeAnimatorController = _victoryController;
        }

        else
        {
            _animator.runtimeAnimatorController = _loseController;
        }
    }
}
