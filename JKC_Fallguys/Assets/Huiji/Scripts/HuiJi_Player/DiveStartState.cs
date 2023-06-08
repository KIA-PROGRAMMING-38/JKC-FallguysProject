using System;
using UnityEngine;

public class DiveStartState : StateMachineBehaviour
{
    [SerializeField] private float _rotationSpeed;
    
    private float _targetRotation = 90;
    private Vector3 _currentRotation;

    public static event Action OnDive; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Upper Layer의 weight를 0으로 설정한다.
        animator.SetLayerWeight(1, 0);
        
        OnDive?.Invoke();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentRotation = animator.transform.rotation.eulerAngles;
        
        // 캐릭터의 아래 방향으로 rotation한다.
        animator.transform.rotation = Quaternion.Lerp(Quaternion.Euler(_currentRotation),
            Quaternion.Euler(_targetRotation, _currentRotation.y, _currentRotation.z),
            Time.deltaTime * _rotationSpeed);
    }
}