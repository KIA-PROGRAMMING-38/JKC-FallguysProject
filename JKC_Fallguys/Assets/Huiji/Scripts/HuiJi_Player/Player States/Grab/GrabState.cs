using UnityEngine;

public class GrabState : StateMachineBehaviour
{
    private Transform _leftHandTarget;
    private Transform _rightHandTarget;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _leftHandTarget = animator.transform.Find("LeftHandTarget");
        _rightHandTarget = animator.transform.Find("RightHandTarget");
    }
    
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // IK Position의 비율을 1로 설정한다.
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        
        // IK Rotation의 비율을 1로 설정한다.
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        
        // 양손의 Target을 정해준다.
        animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandTarget.rotation);
        animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandTarget.rotation);
    }
}
