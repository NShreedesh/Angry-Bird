using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Instance Info")]
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("Level Info")]
    [Min(1)]
    public int gameLevel = 1;

    [Header("State Info")]
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    public static event Action OnVictoryState;
    public static event Action OnLoseState;

    private void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Application.targetFrameRate = 60;
        }

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(this);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Menu:
                OnMenu();
                break;
            case GameState.Play:
                OnPlay();
                break;
            case GameState.Pause:
                OnPause();
                break;
            case GameState.Victory:
                OnVictory();
                break;
            case GameState.Lose:
                OnLose();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void OnMenu()
    {
        
    }

    private void OnPlay()
    {
        Time.timeScale = 1;
    }

    private void OnPause()
    {
        Time.timeScale = 0;
    }

    private void OnVictory()
    {
        OnVictoryState?.Invoke();
    }

    private void OnLose()
    {
        OnLoseState?.Invoke();
    }

    public enum GameState
    {
        Menu,
        Play,
        Pause,
        Victory,
        Lose
    }
}
