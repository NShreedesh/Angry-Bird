using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Bird Info")]
    public bool isLaunched;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem birdParticles;
    [SerializeField] private ParticleSystem woodParticles;
    [SerializeField] private ParticleSystem[] smokeParticles;
    [SerializeField] private float maxSmokeSize;

    [Header("Enemy Damage Info")]
    [Range(1, 3)]
    [SerializeField] private int damageAmount;
    private bool hasHit;

    private void Start()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BirdParticlesSpawn();
        Destroyables(collision);
        Damagables(collision);
    }

    private void Damagables(Collision2D collision)
    {
        if (hasHit) return;

        if (collision.transform.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            hasHit = true;
            damagable.Damage(damageAmount);
        }
    }

    private void Destroyables(Collision2D collision)
    {
        if (rb.velocity.magnitude > 3)
        {
            if (collision.transform.TryGetComponent<IDestroyable>(out IDestroyable destroyable))
            {
                GameObject woodParticle = Instantiate(woodParticles.gameObject, transform.position, Quaternion.identity);
                ParticleSystem particleSystemForWood = woodParticle.GetComponent<ParticleSystem>();
                destroyable.DestoryObject(particleSystemForWood);
            }
        }
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
            var main  = particlesSystemForSmoke.main;  // for particle system size control...
            particlesSystemForSmoke.Stop();
            particlesSystemForSmoke.Play();
        }
    }
}
