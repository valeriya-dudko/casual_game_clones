using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    public bool FirstStart { get; set; } = true;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(this.gameObject);
    }
}
