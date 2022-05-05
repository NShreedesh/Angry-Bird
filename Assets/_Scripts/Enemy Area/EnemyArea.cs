using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    [Header("Enemy Base Info")]
    [SerializeField] private GameObject[] enemyBaseList;

    private void Start()
    {
        TrackEnemyCount.OnEnemyTrack += EnemyCount;

        int levelNumber = GameManager.Instance.gameLevel - 1; 
        if (levelNumber >= enemyBaseList.Length)
        {
            print("All Level Completed (Clear Player Prefs To Restart...)");
            levelNumber = enemyBaseList.Length - 1;
            GameManager.Instance.gameLevel = levelNumber;
            SaveManager.Save(levelNumber);
        }

        GameObject enemyBase = Instantiate(enemyBaseList[levelNumber], transform);
        enemyBase.transform.localPosition = enemyBaseList[levelNumber].transform.position;
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
