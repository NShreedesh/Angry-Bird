using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Damage Enemy Info")]
    [SerializeField] private int health = 2;
    public int Health => health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] damageSprites;

    [Header("Particles Info")]
    [SerializeField] private ParticleSystem enemyDestroyParticle;

    [Header("Death Info")]
    [SerializeField] private float timeToDie = 0.2f;

    private void Start()
    {
        health = damageSprites.Length;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            health = 0;
            spriteRenderer.sprite = damageSprites[damageSprites.Length - health - 1];
            Invoke(nameof(Die), timeToDie);
        }
        else
        {
            spriteRenderer.sprite = damageSprites[damageSprites.Length - health - 1];
        }
    }

    private void Die()
    {
        ParticleSystem particle = Instantiate(enemyDestroyParticle, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
