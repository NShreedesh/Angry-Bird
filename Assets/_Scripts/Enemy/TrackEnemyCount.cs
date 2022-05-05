using System;
using UnityEngine;

public class TrackEnemyCount : MonoBehaviour
{
    [SerializeField] private Transform enemiesParentTransform;
    [SerializeField] private int enemyCount;

    public static event Func<Transform> OnEnemyTrack;

    private void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.Pause) return;
        if (GameManager.Instance.state == GameManager.GameState.Victory) return;

        if (enemiesParentTransform == null)
        {
            enemiesParentTransform = OnEnemyTrack?.Invoke();
            return;
        }

        enemyCount = enemiesParentTransform.childCount;

        if(enemyCount <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Victory);
        }
    }
}
