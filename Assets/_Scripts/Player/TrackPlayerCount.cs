using UnityEngine;

public class TrackPlayerCount : MonoBehaviour
{
    [SerializeField] private Transform playerParentTransform;
    [SerializeField] private int playerCount;

    private void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.Victory) return;
        if (GameManager.Instance.state == GameManager.GameState.Lose) return;
        if (GameManager.Instance.state == GameManager.GameState.Pause) return;

        playerCount = playerParentTransform.childCount;

        if (playerCount <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }
}
