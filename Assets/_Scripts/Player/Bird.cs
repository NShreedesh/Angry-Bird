using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Bird Info")]
    public bool isLaunched;
    [SerializeField] private Rigidbody2D rb;

    [Header("Particles Info")]
    [SerializeField] private ParticleSystem birdParticles;
    [SerializeField] private ParticleSystem[] smokeParticles;

    [Header("Damage Info")]
    public int damageToEnemy = 1;
    public float damageVelocity = 3;

    private void Start()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BirdParticlesSpawn();
    }

    private void BirdParticlesSpawn()
    {
        if (rb.velocity.magnitude > 1)
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
}
