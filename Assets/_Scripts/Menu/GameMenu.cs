using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [Header("Buttons Info")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button[] restartButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button menuButton;

    [Header("Panels Info")]
    [SerializeField] private GameObject frontUIPanel;
    [SerializeField] private GameObject backUIPanel;

    [Header("Animation Info")]
    [SerializeField] private Animator frontUIPanelAnimator;
    private readonly string slideInAnimation = "slideIn";
    private readonly string slideOutAnimation = "slideOut";

    [Header("Game Over UI Info")]
    [SerializeField] private GameObject gameOverUIPanel;

    private void Start()
    {
        gameOverUIPanel.SetActive(false);

        playButton.onClick.AddListener(() =>
        {
            PlayGame();
        });
        pauseButton.onClick.AddListener(() =>
        {
            PauseGame();
        });
        foreach(var restartButton in restartButton)
        {
            restartButton.onClick.AddListener(() =>
            {
                RestartGame();
            });
        }
        nextButton.onClick.AddListener(() =>
        {
            NextLevel();
        });
        menuButton.onClick.AddListener(() =>
        {
            GotoMenu();
        });

        GameManager.OnVictoryState += GameOver;
        GameManager.OnLoseState += GameOver;
    }

    private void PlayGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
        pauseButton.interactable = true;
        frontUIPanelAnimator.SetTrigger(slideOutAnimation);
    }

    private void PauseGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
        pauseButton.interactable = false;
        frontUIPanelAnimator.SetTrigger(slideInAnimation);
    }

    private void RestartGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        GameManager.Instance.gameLevel++;
        SaveManager.Save(GameManager.Instance.gameLevel);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
        RestartGame();
    }

    private void GameOver()
    {
        if(GameManager.Instance.state == GameManager.GameState.Lose)
        {
            nextButton.interactable = false;
        }
        else if(GameManager.Instance.state == GameManager.GameState.Victory)
        {
            nextButton.interactable = true;
        }
        gameOverUIPanel.SetActive(true);
    }

    private void GotoMenu()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Menu);
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        GameManager.OnVictoryState -= GameOver;
        GameManager.OnLoseState -= GameOver;
    }
}
