using LiteralRepository;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnimationEventController : MonoBehaviour
{
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(PathLiteral.Lobby);
    }
}
