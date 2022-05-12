using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    [Header("Enemy Base Info")]
    [SerializeField] private GameObject[] enemyBaseList;

    private void Start()
    {
        TrackEnemyCount.OnEnemyTrack += EnemyCount;

        int levelNumber = GameManager.Instance.gameLevel; 
        if (levelNumber >= enemyBaseList.Length)
        {
            levelNumber = enemyBaseList.Length;
            GameManager.Instance.gameLevel = levelNumber;
            SaveManager.Save(levelNumber);
        }

        GameObject enemyBase = Instantiate(enemyBaseList[levelNumber - 1], transform);
        enemyBase.transform.localPosition = enemyBaseList[levelNumber - 1].transform.position;
    }

    private Transform EnemyCount()
    {
        return GameObject.FindGameObjectWithTag("Enemies").transform;
    }

    private void OnDisable()
    {
        TrackEnemyCount.OnEnemyTrack -= EnemyCount;
    }
}
