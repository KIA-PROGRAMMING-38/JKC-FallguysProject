using UnityEngine;
using Util.Helper;

public class PlayerRepository : MonoBehaviour
{
    public void Clear()
    {
        GameObjectHelper.DestroyAllChildren(gameObject);
    }
}
