using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Info")]
    public InputControls inputControls;

    [Header("Bird Spawn Info")]
    [SerializeField] private int howManyBirds = 1;
    [SerializeField] private Transform birdSpawnTransform;
    [SerializeField] private float birdOffset;
    public List<Bird> birds = new();

    [Header("Bird Info")]
    public GameObject birdPrefab;
    public Transform birdLaunchTransform;
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
            GameObject bird = Instantiate(birdPrefab, birdLaunchTransform.position, Quaternion.identity, birdSpawnTransform);
            bird.GetComponent<Rigidbody2D>().MovePosition(birdSpawnTransform.position - new Vector3(birdOffset * (i + 1), 0));
            bird.TryGetComponent<Bird>(out Bird currentBird);

            birds.Add(currentBird);

            currentBird.canBeLaunched = false;
        }

        MakeBirdReady();
    }

    public void MakeBirdReady()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if(birds[i].canBeLaunched == false)
            {
                birds[i].GetComponent<Rigidbody2D>().MovePosition(birdLaunchTransform.position);
                birds[i].canBeLaunched = true;
                return;
            }
        }
    }
}
