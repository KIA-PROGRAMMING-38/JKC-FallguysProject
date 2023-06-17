using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LobbySceneFallguyController : MonoBehaviour
{
    private CancellationTokenSource _cancellationTokenSource;
    private Vector3 _startPosition;
    private Vector3 _endPosition = new Vector3(-0.1f, -2.8f, 0f);

    [SerializeField] 
    private float _fallEffectDuration;
    
    private void Awake()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _startPosition = transform.position;
        MoveToStartPosition(_cancellationTokenSource.Token).Forget();
    }

    private async UniTaskVoid MoveToStartPosition(CancellationToken cancellationToken)
    {
        float elapsedTime = 0;
        
        while (elapsedTime < _fallEffectDuration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(_startPosition, _endPosition, elapsedTime / _fallEffectDuration);
            
            await UniTask.Yield(cancellationToken : cancellationToken);
        }
        
        transform.position = _endPosition;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
