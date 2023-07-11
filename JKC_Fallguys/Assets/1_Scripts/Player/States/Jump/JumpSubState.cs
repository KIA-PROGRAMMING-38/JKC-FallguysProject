using LiteralRepository;
using UnityEngine;

public class JumpSubState : StateMachineBehaviour
{
    private PlayerInputController _playerInputController;
    private PlayerPhysicsController _playerPhysicsController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInputController = animator.GetComponentInParent<PlayerInputController>();
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
        _playerPhysicsController.ZeroizeDiveHeightForce();
    }
    
    // JumpState동안 Dive 키를 누르면 Dive로 전이할 수 있게 한다.
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerInputController.IsDive)
        {
            animator.SetBool(AnimLiteral.IsDiving, true);
        }
    }
}
