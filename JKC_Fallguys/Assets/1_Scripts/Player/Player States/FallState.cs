using UnityEngine;

public class FallState : StateMachineBehaviour
{
    private PlayerPhysicsController _playerPhysicsController;
    private PlayerInput _playerInput;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
        _playerInput = animator.GetComponentInParent<PlayerInput>();
        
        _playerPhysicsController.UnfreezeRotationAxis();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
