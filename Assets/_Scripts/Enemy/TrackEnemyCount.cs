using UnityEngine;

public class TrackEnemyCount : MonoBehaviour
{
    [SerializeField] private Transform enemiesParentTransform;
    [SerializeField] private int enemyCount;

    private void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.Victory) return;

        enemyCount = enemiesParentTransform.childCount;

        if(enemyCount <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Victory);
        }
    }
}
