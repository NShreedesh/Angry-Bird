using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
}

public enum GameState
{
    Play,
    Pause,
    Victory,
    Lose
}
