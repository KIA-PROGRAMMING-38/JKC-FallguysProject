using LiteralRepository;
using Model;
using UniRx;

public class ResultPresenter : Presenter
{
    private ResultView _resultView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _resultView = view as ResultView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        
    }

    /// <summary>
    /// 이전 씬에서 승리 여부에 따라 텍스트 이미지를 바인딩합니다.
    /// </summary>
    protected override void OnUpdatedModel()
    {
        ResultSceneModel.IsVictorious.Where(isVictorious => isVictorious == true)
            // .First()
            .Subscribe(_ => LoadVictorySprite())
            .AddTo(_compositeDisposable);
        
        ResultSceneModel.IsVictorious.Where(isVictorious => isVictorious == false)
            // .First()
            .Subscribe(_ => LoadLoseSprite())
            .AddTo(_compositeDisposable);
    }

    private void LoadVictorySprite()
    {
        _resultView.ResultTextImage.sprite = 
            DataManager.GetSpriteData(PathLiteral.UI, PathLiteral.GameResult, PathLiteral.VictoryTextImage);
    }
    
    private void LoadLoseSprite()
    {
        _resultView.ResultTextImage.sprite = 
            DataManager.GetSpriteData(PathLiteral.UI, PathLiteral.GameResult, PathLiteral.LoseTextImage);
    }
    
    public override void OnRelease()
    {
        _resultView = default;
        _compositeDisposable.Dispose();
    }
}