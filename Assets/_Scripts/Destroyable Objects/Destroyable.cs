using UnityEngine;

public class Destroyable : MonoBehaviour, IDestroyable, IBombable
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Enemy Hit Info")]
    [SerializeField] private float enemyHitVelocity = 2;
    [SerializeField] private int damageAmount = 1;

    [Header("Destroy Info")]
    [SerializeField] private int health;
    public int Health => health;
    [SerializeField] private ParticleSystem woodParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHit(collision);
    }

    private void EnemyHit(Collision2D collision)
    {
        if (!collision.transform.TryGetComponent<IDamagable>(out IDamagable damagable)) return;
        if (!collision.transform.TryGetComponent<Rigidbody2D>(out Rigidbody2D enemyRb)) return;

        if (rb.velocity.magnitude >= enemyHitVelocity || enemyRb.velocity.magnitude >= enemyHitVelocity)
        {
            damagable.Damage(damageAmount);
        }
    }

    public void Destroy()
    {
        ParticleSystem particle = Instantiate(woodParticle, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(gameObject);
    }
}
