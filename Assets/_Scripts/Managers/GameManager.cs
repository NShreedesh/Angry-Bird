using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public static event Action<GameState> OnGameStateChanged;

    public GameState state;

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
                break;
            case GameState.Pause:
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

    private void OnVictory()
    {
        print("Victory");
    }

    private void OnLose()
    {
        print("Lose");
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
