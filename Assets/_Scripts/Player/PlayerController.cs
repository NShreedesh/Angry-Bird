using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Info")]
    public InputControls inputControls;

    [Header("Bird Spawn Info")]
    [SerializeField] private int howManyBirds = 1;
    [SerializeField] private Transform birdSpawnPosition;
    [SerializeField] private float birdOffset;
    [SerializeField] private List<Bird> birds = new List<Bird>();

    [Header("Bird Info")]
    public GameObject birdPrefab;
    public Transform birdSpawnTransform;
    public Bird bird;

    [Header("Physics Info")]
    public Rigidbody2D rb;

    private void Start()
    {
        SpawnBird();
    }

    public void SpawnBird()
    {
        for (int i = 0; i < howManyBirds; i++)
        {
            GameObject bird = Instantiate(birdPrefab.gameObject, birdSpawnTransform.position, Quaternion.identity);
            bird.transform.position = birdSpawnPosition.position - new Vector3(birdOffset * (i+1), 0);
            bird.TryGetComponent<Bird>(out Bird currentBird);

            birds.Add(currentBird);
            currentBird.isLaunched = true;
        }

        MakeBirdReady();
    }

    public void MakeBirdReady()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if(birds[i].isLaunched == true)
            {
                birds[i].transform.position = birdSpawnTransform.position;
                birds[i].isLaunched = false;
                birds.Remove(birds[i]);
                return;
            }
        }
    }
}
