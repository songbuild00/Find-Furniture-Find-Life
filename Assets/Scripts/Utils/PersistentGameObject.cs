using UnityEngine;

public class PersistentGameObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
