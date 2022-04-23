using UnityEngine;

public class Enemy : MonoBehaviour
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


    private void Start()
    {
        health = damageSprites.Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<Bird>(out Bird bird))
        {
            if (collision.relativeVelocity.magnitude < bird.damageVelocity) return;
            Damage(bird.damageToEnemy);
        }

        if (collision.transform.TryGetComponent<Destroyable>(out Destroyable destroyable))
        {
            if(collision.GetContact(0).normal.y < 0.5f)
            {
                //When Destroyables hits the enemy.
                if (collision.relativeVelocity.magnitude < destroyable.damageVelocity) return;
                Damage(destroyable.damageToEnemy);
            }
            else
            {
                //When Enemy Hits the destroyables.
                if (collision.relativeVelocity.magnitude < (destroyable.damageVelocity + 2)) return;
                Damage(destroyable.damageToEnemy);
            }
        }

        if (collision.transform.TryGetComponent<Ground>(out Ground ground))
        {
            if (collision.relativeVelocity.magnitude < ground.damageVelocity) return;
            Damage(ground.damageToEnemy);
        }

        if (collision.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (collision.relativeVelocity.magnitude < enemy.damageVelocity) return;
            Damage(enemy.damageToEnemy);
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
