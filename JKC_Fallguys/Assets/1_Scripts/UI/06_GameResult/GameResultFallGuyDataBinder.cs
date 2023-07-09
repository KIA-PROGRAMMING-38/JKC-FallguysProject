using System.IO;
using LiteralRepository;
using Model;
using ResourceRegistry;
using UnityEngine;

public class GameResultFallGuyDataBinder : MonoBehaviour
{
    private Animator _animator;
    private RuntimeAnimatorController _victoryController;
    private RuntimeAnimatorController _loseController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 승리 여부에 따라 애니메이션 컨트롤러를 바인딩합니다.
    /// </summary>
    private void Start()
    {
        if (ResultSceneModel.IsVictorious.Value)
        {
            _animator.runtimeAnimatorController = 
                Resources.Load<RuntimeAnimatorController>(Path.Combine("Animation", "Controller", PathLiteral.GameResultAnimator, PathLiteral.VictoryAnimatorController));
        }

        else
        {
            _animator.runtimeAnimatorController = 
                Resources.Load<RuntimeAnimatorController>(Path.Combine("Animation", "Controller", PathLiteral.GameResultAnimator, PathLiteral.LoseAnimatorController));
        }
    }
}
