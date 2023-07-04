using UnityEngine;

public class JumpingState : StateMachineBehaviour
{
    private PlayerPhysicsController _playerPhysicsController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
        _playerPhysicsController.ActivateOnJumpingAction();
    }
    
    override public void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
    {
        _playerPhysicsController.jumpCancellationTokenSource.Cancel();
    }
}
