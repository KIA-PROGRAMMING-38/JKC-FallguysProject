using UnityEngine;

public class FallState : StateMachineBehaviour
{
    private PlayerPhysicsController _playerPhysicsController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();

        if ( _playerPhysicsController.jumpCancellationTokenSource != null )
        {
            _playerPhysicsController.jumpCancellationTokenSource.Cancel();
        }
        
        _playerPhysicsController.UnfreezeRotationAxis();
    }
}
