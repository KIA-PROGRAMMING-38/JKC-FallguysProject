using UnityEngine;
using UnityEngine.UI;

public class RoundResultView : View
{
    public Text FirstScore { get; private set; }
    public Text SecondScore { get; private set; }
    public Text ThirdScore { get; private set; }
    
    public Text FirstRankingText { get; private set; }
    public Text SecondRankingText { get; private set; }
    public Text ThirdRankingText { get; private set; }
    
    public Text FirstPlayerNameText { get; private set; }
    public Text SecondPlayerNameText { get; private set; }
    public Text ThirdPlayerNameText { get; private set; }
    
    public RectTransform RoundResultCanvasRect { get; private set; }

    private void Awake()
    {
        FirstScore = transform.Find("FirstScore").GetComponent<Text>();
        SecondScore = transform.Find("SecondScore").GetComponent<Text>();
        ThirdScore = transform.Find("ThirdScore").GetComponent<Text>();

        FirstRankingText = transform.Find("FirstRankingText").GetComponent<Text>();
        SecondRankingText = transform.Find("SecondRankingText").GetComponent<Text>();
        ThirdRankingText = transform.Find("ThirdRankingText").GetComponent<Text>();

        FirstPlayerNameText = transform.Find("FirstRankingText").transform.Find("FirstPlayerNameText").GetComponent<Text>();
        SecondPlayerNameText = transform.Find("SecondRankingText").transform.Find("SecondPlayerNameText").GetComponent<Text>();
        ThirdPlayerNameText = transform.Find("ThirdRankingText").transform.Find("ThirdPlayerNameText").GetComponent<Text>();

        RoundResultCanvasRect = GetComponent<RectTransform>();
    }
}
