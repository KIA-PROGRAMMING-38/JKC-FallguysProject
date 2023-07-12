using UnityEngine;

public static class GameObjectHelper
{
    public static void SetActiveGameObject(GameObject gameObject, bool status)
    {
        gameObject.SetActive(status);
    }
    
    public static void DestroyAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (child != null)
                GameObject.Destroy(child.gameObject);
        }
    }
}
