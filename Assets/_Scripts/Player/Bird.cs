using UnityEngine;

public class Bird : MonoBehaviour, IDamagable
{
    [Header("Bird Info")]
    public bool canBeLaunched;
    public bool isLaunched;
    [SerializeField] private Rigidbody2D rb;

    [Header("Particles Info")]
    [SerializeField] private ParticleSystem birdParticles;
    [SerializeField] private ParticleSystem[] smokeParticles;

    [Header("Damage Info")]
    public int damageToEnemy = 1;
    public float damageVelocity = 3;
    public int DamageToEnemy { get { return damageToEnemy; } }
    public float DamageVelocity { get { return damageVelocity; } }

    [Header("Bird Destory Info")]
    [SerializeField] private bool isDestroyed;

    private void Start()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (!isLaunched) return;
        if (isDestroyed) return;

        if(rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            BirdParticlesSpawn();
            Destroy(gameObject);
            isDestroyed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.magnitude > 1)
        {
            BirdParticlesSpawn();
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
}
