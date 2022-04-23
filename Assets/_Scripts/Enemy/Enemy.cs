using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Health Info")]
    [SerializeField] private int health;

    [Header("Damage Sprite Change Info")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] damageSprites;

    [Header("Particles Info")]
    [SerializeField] private ParticleSystem destroyParticle;

    [Header("Damage Info")]
    public int damageToEnemy = 1;
    public float damageVelocity = 3;
    public int DamageToEnemy { get { return damageToEnemy; } }
    public float DamageVelocity { get { return damageVelocity; } }

    private void Start()
    {
        health = damageSprites.Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            if (collision.relativeVelocity.magnitude < damagable.DamageVelocity) return;

            Damage(damagable.DamageToEnemy);
        }
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            health = 0;
            Die();
            return;
        }

        spriteRenderer.sprite = damageSprites[^health];
    }

    public void Die()
    {
        ParticleSystem particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        var main = particle.main;
        main.startSize = Random.Range(1f,1.2f);
        particle.Stop();
        particle.Play();
        Destroy(gameObject);
    }
}
