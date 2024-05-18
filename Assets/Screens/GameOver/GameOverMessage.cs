using UnityEngine;

public class GameOverMessage : MonoBehaviour
{
    public static GameOverMessage Instance { get; private set; }

    public string message;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
