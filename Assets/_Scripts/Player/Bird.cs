using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Bird Info")]
    public bool canBeLaunched;
    public bool isLaunched;
    [SerializeField] private Rigidbody2D rb;

    [Header("Bird Particles Info")]
    [SerializeField] private ParticleSystem birdParticles;
    [SerializeField] private ParticleSystem[] smokeParticles;
    [SerializeField] private float birdParticlesVelocity = 1;

    [Header("Bird Destory Info")]
    public bool isDestroyed;

    [Header("Enemy Hit Info")]
    [SerializeField] private float enemyHitVelocity = 2;
    [SerializeField] private int damageAmount = 1;

    [Header("Destroyable Hit Info")]
    [SerializeField] private float destroyableHitVelocity = 2;

    [Header("Collision Info")]
    private bool isCollided;

    [Header("Trajectory Info")]
    [SerializeField] private float totalTime = 0.1f;
    private float time;
    private Trajectory trajectory;

    private void Awake()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    private void Start()
    {
        trajectory = GameObject.FindGameObjectWithTag("Trajectory").GetComponent<Trajectory>();
    }

    private void Update()
    {
        if (!isLaunched) return;
        if (isDestroyed) return;

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            BirdParticlesSpawn();
            Destroy(gameObject);
            isDestroyed = true;
        }

        HandleTrajectory();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollided = true;

        if (rb.velocity.magnitude >= birdParticlesVelocity)
        {
            BirdParticlesSpawn();
        }

        if(rb.velocity.magnitude > enemyHitVelocity)
        {
            EnemyHit(collision);
        }

        if (rb.velocity.magnitude > destroyableHitVelocity)
        {
            DestroyableHit(collision);
        }
    }

    private void BirdParticlesSpawn()
    {
        GameObject birdParticle = Instantiate(birdParticles.gameObject, transform.position, Quaternion.identity);
        ParticleSystem particlesSystemForBird = birdParticle.GetComponent<ParticleSystem>();
        particlesSystemForBird.Stop();
        particlesSystemForBird.Play();

        GameObject smokeParticle = Instantiate(smokeParticles[Random.Range(0, smokeParticles.Length)].gameObject, transform.position, Quaternion.identity);
        ParticleSystem particlesSystemForSmoke = smokeParticle.GetComponent<ParticleSystem>();
        particlesSystemForSmoke.Stop();
        particlesSystemForSmoke.Play();
    }

    private void EnemyHit(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.Damage(damageAmount);
        }
    }

    private void DestroyableHit(Collision2D collision)
    {
        if (!collision.transform.TryGetComponent<IDestroyable>(out IDestroyable destroyable)) return;

        destroyable.Destroy();
    }

    private void HandleTrajectory()
    {
        if (isCollided) return;

        time += Time.deltaTime;
        if (time >= totalTime)
        {
            trajectory.TrajectoryPlacement(transform);
            time = 0;
        }
    }
}
