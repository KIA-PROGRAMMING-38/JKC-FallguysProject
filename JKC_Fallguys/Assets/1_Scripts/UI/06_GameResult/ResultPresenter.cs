using System.IO;
using LiteralRepository;
using Model;
using UniRx;
using UnityEngine;

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
        ResultSceneModel.IsVictorious
            .Subscribe(isVictorious => LoadResultSprite(isVictorious ? PathLiteral.VictoryTextImage : PathLiteral.LoseTextImage))
            .AddTo(_compositeDisposable);
    }
    
    private void LoadResultSprite(string spriteName)
    {
        string path = Path.Combine(PathLiteral.Sprites, PathLiteral.UI, PathLiteral.GameResult, spriteName);
        _resultView.ResultTextImage.sprite = Resources.Load<Sprite>(path);
    }
    
    public override void OnRelease()
    {
        _resultView = default;
        _compositeDisposable.Dispose();
    }
}