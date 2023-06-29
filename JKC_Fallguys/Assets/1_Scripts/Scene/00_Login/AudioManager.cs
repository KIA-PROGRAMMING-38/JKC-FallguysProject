using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    private AudioSource _audioSource;
    private AudioClip _loginSound;
    
    public AudioClip[] lobbyMusic;

    protected override void Awake()
    {
        base.Awake();
        
        // 오디오 컴포넌트 부착시켜야 함.
        
        // _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += test;
    }

    private void test(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("로드 씬");
    }
    
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}
