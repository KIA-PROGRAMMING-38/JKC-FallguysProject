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
        // 공중에서 인풋에 따라 움직일 수 있게 한다.
        _playerPhysicsController.OnJumping();
    }
}
