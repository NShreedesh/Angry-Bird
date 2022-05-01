using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [Header("Buttons Info")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;

    [Header("Buttons Info")]
    [SerializeField] private GameObject frontUIPanel;
    [SerializeField] private GameObject backUIPanel;

    [Header("Animation Info")]
    [SerializeField] private Animator frontUIPanelAnimator;
    private string slideInAnimation = "slideIn";
    private string slideOutAnimation = "slideOut";

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            PlayGame();
        });
        pauseButton.onClick.AddListener(() =>
        {
            PauseGame();
        });
    }

    private void PlayGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
        pauseButton.interactable = true;
        frontUIPanelAnimator.SetTrigger(slideOutAnimation);
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
        pauseButton.interactable = false;
        frontUIPanelAnimator.SetTrigger(slideInAnimation);
        Time.timeScale = 0;
    }
}
