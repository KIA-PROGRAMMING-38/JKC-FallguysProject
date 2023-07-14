using UnityEngine;

namespace Util.Helper
{
    public static class GameObjectHelper
    {
        public static GameObject CreateRepository(string name, Transform parent, Vector3 position = default, Quaternion rotation = default)
        {
            GameObject newObject = new GameObject(name);
            newObject.transform.position = position;
            newObject.transform.rotation = rotation;
            newObject.transform.SetParent(parent);
            return newObject;
        }

        public static T CreateRepository<T>(string name, Transform parent, Vector3 position = default, Quaternion rotation = default) where T : Component
        {
            GameObject newObject = CreateRepository(name, parent, position, rotation);
            T component = newObject.AddComponent<T>();
            return component;
        }

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
}