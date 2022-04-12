using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Info")]
    public InputControls inputControls;

    [Header("Bird Info")]
    public GameObject birdPrefab;
    public Transform birdSpawnTransform;
    [HideInInspector] public Bird bird;

    [Header("Physics Info")]
    public Rigidbody2D rb;

    private void Start()
    {
        SpawnBird();
    }

    public void SpawnBird()
    {
        Instantiate(birdPrefab.gameObject, birdSpawnTransform.position, Quaternion.identity);
    }
}
