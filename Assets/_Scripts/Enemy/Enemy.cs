using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Enemy Health Info")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] damageSpites;
    [SerializeField] private int health;

    [Header("Particle System Info")]
    [SerializeField] private ParticleSystem enemyDeathParticle;

    private void Start()
    {
        health = damageSpites.Length;
    }

    public void Damage(int damageAmount)
    {
        if (health <= 0)
        {
            Die();
            return;
        }

        health -=damageAmount;
        spriteRenderer.sprite = damageSpites[damageSpites.Length - health - 1];
    }

    private void Die()
    {
        ParticleSystem particle = Instantiate(enemyDeathParticle, transform.position, Quaternion.identity);
        var main = particle.main;
        main.startSize = 1;
        particle.Play();
        Destroy(gameObject);
    }
}
