using Photon.Pun;
using UnityEngine.SceneManagement;

public static class SceneChangeHelper
{
    public static void ChangeLocalScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void ChangeNetworkLevel(int sceneIndex)
    {
        PhotonNetwork.LoadLevel(sceneIndex);
    }
}
