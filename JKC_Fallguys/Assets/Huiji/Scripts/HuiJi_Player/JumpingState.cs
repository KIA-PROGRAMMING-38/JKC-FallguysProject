using UnityEngine;

public class JumpingState : StateMachineBehaviour
{
    private PlayerPhysicsController _playerPhysicsController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController.OnJumping();
    }
}
