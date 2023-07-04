using UnityEngine;

public class ObserverCamera : MonoBehaviour
{
    private Transform _followPlayerCharacter;

    public void UpdatePlayerTarget(Transform followPlayerCharacter)
    {
        _followPlayerCharacter = followPlayerCharacter;
    }
    
    private void LateUpdate()
    {
        FollowPlayerBody();
    }
    
    private void FollowPlayerBody()
    {
        Vector3 targetPos = new Vector3
            (_followPlayerCharacter.position.x, _followPlayerCharacter.position.y - 1, _followPlayerCharacter.position.z);
        transform.position = targetPos;
    }
}
