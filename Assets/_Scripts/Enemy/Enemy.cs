using System.Threading.Tasks;
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
            Die();
        }
        else
        {
            spriteRenderer.sprite = damageSprites[damageSprites.Length - health];
        }
    }

    private void Die()
    {
        ParticleSystem particle = Instantiate(enemyDestroyParticle, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(gameObject);
    }
}
