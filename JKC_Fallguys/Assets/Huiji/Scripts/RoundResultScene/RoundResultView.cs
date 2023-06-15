using UnityEngine.UI;

public class RoundResultView : View
{
    public Text FirstScore { get; private set; }
    public Text SecondScore { get; private set; }
    public Text ThirdScore { get; private set; }

    private void Awake()
    {
        FirstScore = transform.Find("FirstScore").GetComponent<Text>();
        SecondScore = transform.Find("SecondScore").GetComponent<Text>();
        ThirdScore = transform.Find("ThirdScore").GetComponent<Text>();
    }
}
