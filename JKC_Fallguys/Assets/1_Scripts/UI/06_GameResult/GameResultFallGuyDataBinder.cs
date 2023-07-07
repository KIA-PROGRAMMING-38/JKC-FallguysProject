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
                ResourceManager.Load<RuntimeAnimatorController>(PathLiteral.AnimatorController, PathLiteral.GameResultAnimator, PathLiteral.VictoryAnimatorController);
            FallGuyAudioClips = AudioRegistry.VictoryFallGuySFX;
        }

        else
        {
            _animator.runtimeAnimatorController = 
                ResourceManager.Load<RuntimeAnimatorController>(PathLiteral.AnimatorController, PathLiteral.GameResultAnimator, PathLiteral.LoseAnimatorController);
            FallGuyAudioClips = AudioRegistry.LoseFallGuySFX;
        }
    }

    public AudioClip[] FallGuyAudioClips { get; private set; }
}
