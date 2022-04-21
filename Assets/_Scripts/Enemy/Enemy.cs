using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Enemy Health Info")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] damageSpites;
    [SerializeField] private int health;

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
        Destroy(gameObject);
    }
}
