using System.Threading;
using Cysharp.Threading.Tasks;
using Model;
using UnityEngine;

public class MatchingSceneFallguyController : MonoBehaviour
{
    private CancellationTokenSource _cancellationTokenSource;
    private Vector3 _startPosition;
    private Vector3 _endPosition = new Vector3(0f, 0.6f, -1.3f);

    [SerializeField] 
    private float _fallEffectDuration;
    [SerializeField] 
    private SkinnedMeshRenderer _bodyRenderer;
    
    private void Awake()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _startPosition = transform.position;
        MoveToStartPosition(_cancellationTokenSource.Token).Forget();

        _bodyRenderer.material.mainTexture = PlayerTextureRegistry.PlayerTextures[LobbySceneModel.PlayerTextureIndex.Value];
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
