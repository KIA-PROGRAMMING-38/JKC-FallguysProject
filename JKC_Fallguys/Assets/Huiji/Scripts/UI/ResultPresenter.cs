using LiteralRepository;
using Model;
using UniRx;

public class ResultPresenter : Presenter
{
    private ResultView _resultView;
    public override void OnInitialize(View view)
    {
        _resultView = view as ResultView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        // 유저 이벤트 없음.
    }

    protected override void OnUpdatedModel()
    {
        ResultSceneModel.IsVictorious.Where(isVictorious => isVictorious == true)
            .Subscribe(_ => LoadVictorySprite());
        
        ResultSceneModel.IsVictorious.Where(isVictorious => isVictorious == false)
            .Subscribe(_ => LoadLoseSprite());
    }

    private void LoadVictorySprite()
    {
        _resultView.ResultTextImage.sprite = DataManager.GetSpriteData(PathLiteral.UI, "GameResult", "VictoryText");
    }
    
    private void LoadLoseSprite()
    {
        _resultView.ResultTextImage.sprite = DataManager.GetSpriteData(PathLiteral.UI, "GameResult", "LoseText");
    }
    
    public override void OnRelease()
    {
        // 스트림 아직 없음.
    }
}