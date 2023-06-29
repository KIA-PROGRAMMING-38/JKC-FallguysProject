using LiteralRepository;
using Photon.Pun;
using ResourceRegistry;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoginPanelPresenter : Presenter
{
    private LoginPanelView _loginPanelView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _loginPanelView = view as LoginPanelView;
        
        // InputField의 입력값에 대한 초기화 진행
        Model.LoginSceneModel.NotConditionEstablished();

        InitializeRx();
    }

    /// <summary>
    /// Login을 시도할 경우 호출되는 함수입니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _loginPanelView.StartGameButton
            .OnClickAsObservable()
            .Subscribe(x => TryEnterLobby())
            .AddTo(_compositeDisposable);
    }

    /// <summary>
    /// 사용자가 로비에 접속을 시도할 때 호출되는 메서드입니다.
    /// </summary>
    private void TryEnterLobby()
    {
        // 사용자가 닉네임을 올바르게 입력한 경우, 닉네임을 설정하고 로비에 접속을 시도합니다.
        if (IsCorrectInput())
        {
            SetPlayerNickName();
            Model.LoginSceneModel.ConditionEstablished();
            
            // 로그인 사운드 플레이
            AudioSource loginButtonAudioSource = _loginPanelView.StartGameButton.gameObject.GetComponent<AudioSource>();
            loginButtonAudioSource.PlayOneShot(AudioRegistry.LoginSFX);
            
            SceneManager.LoadScene(SceneIndex.Lobby); // Lobby
        }
        // 사용자가 닉네임을 올바르게 입력하지 않은 경우, 상태를 NotConditionEstablished로 변경합니다.
        else
        {
            Model.LoginSceneModel.NotConditionEstablished();
        }
    }

    /// <summary>
    /// 사용자가 입력한 닉네임이 조건에 부합하는지 검증합니다.
    /// </summary>
    /// <returns>닉네임이 1자 이상 10자 이하이면 true를 반환하고, 그렇지 않으면 false를 반환합니다.</returns>
    private bool IsCorrectInput()
    {
        if (_loginPanelView.PlayerNameInputField.text.Length > 0 &&
            _loginPanelView.PlayerNameInputField.text.Length <= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 사용자가 입력한 데이터를 바탕으로 플레이어의 닉네임을 세팅합니다.
    /// </summary>
    private void SetPlayerNickName()
    {
        PhotonNetwork.NickName = _loginPanelView.PlayerNameInputField.text;
    }
    
    /// <summary>
    /// Model에서 사용자 입력에 대한 업데이트를 받고, 이를 View에 반영하는 메서드입니다.
    /// 사용자가 잘못된 입력을 제공하면 에러 패널이 활성화됩니다.
    /// 이미 에러 패널이 활성화된 상태에서 사용자가 다시 클릭 이벤트를 발생시키면 에러 패널이 비활성화됩니다. 
    /// </summary>
    protected override void OnUpdatedModel()
    {
        _loginPanelView.StartGameButton
            .OnClickAsObservable()
            .Where(_ => !Model.LoginSceneModel.IsLoginInputFieldFilled)
            .Subscribe(_ => SetActiveErrorPanel(true))
            .AddTo(_compositeDisposable);
        
        _loginPanelView.NameConventionGuideTouchPanel
            .OnClickAsObservable()
            .Where(_ => !Model.LoginSceneModel.IsLoginInputFieldFilled)
            .Subscribe(_ => SetActiveErrorPanel(false))
            .AddTo(_compositeDisposable);
    }
    
    private void SetActiveErrorPanel(bool currentInputFieldState)
    {
        _loginPanelView.NameConventionGuideTouchPanel.gameObject.SetActive(currentInputFieldState);
    }
    
    public override void OnRelease()
    {
        _loginPanelView = default;
        _compositeDisposable.Dispose();
    }
}
