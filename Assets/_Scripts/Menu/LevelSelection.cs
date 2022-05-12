using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Button levelSelectButton;
    [SerializeField] private int totalNumberOfLevels;

    private void Start()
    {
        SpawnButtonLevel();
    }

    private void SpawnButtonLevel()
    {
        for(int level = 0; level < totalNumberOfLevels; level++)
        {
            Button button = Instantiate(levelSelectButton, transform);
            int levelNumber = level + 1;
            button.GetComponentInChildren<TMP_Text>().text = levelNumber.ToString();
            button.onClick.AddListener(async () => await OnLevelButtonPressed(levelNumber));
        }
    }

    private async Task OnLevelButtonPressed(int levelNumber)
    {
        GameManager.Instance.gameLevel = levelNumber;
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
        await Task.Delay(300);
        SceneManager.LoadSceneAsync(1);
    }
}
