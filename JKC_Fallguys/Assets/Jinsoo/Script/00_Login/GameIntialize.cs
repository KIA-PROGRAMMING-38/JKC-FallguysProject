using UnityEngine;

public class GameIntialize : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }
}
