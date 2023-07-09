using System.Threading;
using Cysharp.Threading.Tasks;
using Model;
using UniRx;
using UnityEngine;

public class LobbySceneFallguyController : MonoBehaviour
{
    private SkinnedMeshRenderer _fallGuyBody; 
    private CancellationTokenSource _cancellationTokenSource;
    private Vector3 _centerPosition = new Vector3(-0.1f, -2.8f, 0f);
    private Vector3 _customizationPosition = new Vector3(-5.6f, -2.8f, 0f);

    [SerializeField] 
    private float _homeStateMoveDuration;
    [SerializeField]
    private float _customizationStateMoveDurattion;
    
    private void Awake()
    {
        _fallGuyBody = GetComponentInChildren<SkinnedMeshRenderer>();
        
        _cancellationTokenSource = new CancellationTokenSource();

        InitializeRx();
    }

    private void InitializeRx()
    {
        LobbySceneModel.CurrentLobbyState
            .Where(state => state == LobbySceneModel.LobbyState.Customization)
            .Subscribe(_ => CustomizationState())
            .AddTo(this);
        
        LobbySceneModel.CurrentLobbyState
            .Where(state => state == LobbySceneModel.LobbyState.Home)
            .Subscribe(_ => HomeState())
            .AddTo(this);

        LobbySceneModel.PlayerTextureIndex
            .Subscribe(_ => _fallGuyBody.material.mainTexture = PlayerTextureRegistry.PlayerTextures[LobbySceneModel.PlayerTextureIndex.Value])
            .AddTo(this);
    }

    private void CustomizationState()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        MoveToPosition
            (transform.position, _customizationPosition, _customizationStateMoveDurattion,_cancellationTokenSource.Token).Forget();
    }
    
    private void HomeState()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        MoveToPosition
            (transform.position, _centerPosition, _homeStateMoveDuration,_cancellationTokenSource.Token).Forget();
    }
    
    private async UniTaskVoid MoveToPosition(Vector3 startPos, Vector3 endPos, float duration, CancellationToken cancellationToken)
    {
        float elapsedTime = 0;
        Vector3 capturePos = startPos;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(capturePos, endPos, elapsedTime / duration);
            
            await UniTask.Yield(cancellationToken : cancellationToken);
        }
        
        transform.position = endPos;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
