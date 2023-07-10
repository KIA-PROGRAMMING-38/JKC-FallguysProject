using UnityEngine;

public static class GameObjectHelper
{
    public static void SetActiveGameObject(GameObject gameObject, bool status)
    {
        gameObject.SetActive(status);
    }
}
