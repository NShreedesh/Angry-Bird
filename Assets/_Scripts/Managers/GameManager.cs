using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Instance Info")]
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("State Info")]
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    public static event Action OnVictoryState;
    public static event Action OnLoseState;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 61;
        }

        UpdateGameState(GameState.Play);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Menu:
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
        print("Victory");
        OnVictoryState?.Invoke();
    }

    private void OnLose()
    {
        print("Lose");
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
