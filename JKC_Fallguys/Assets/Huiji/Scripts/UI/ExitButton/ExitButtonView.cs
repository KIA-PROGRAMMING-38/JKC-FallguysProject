using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ExitButtonView : View
{
    public Button UIPopUpButton { get; private set; }

    [SerializeField] private StageExitPanelViewController _stageExitPanel;
    public StageExitPanelViewController StageExitPanel => _stageExitPanel;

    private void Awake()
    {
        UIPopUpButton = transform.Find("UIPopUpButton").GetComponent<Button>();
    }
}
