using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Enemy Hit Info")]
    [SerializeField] private float enemyHitVelocity = 2;
    [SerializeField] private int damageAmount = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHit(collision);
    }

    private void EnemyHit(Collision2D collision)
    {
        if (!collision.transform.TryGetComponent<IDamagable>(out IDamagable damagable)) return;
        if (!collision.transform.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) return;

        if(rb.velocity.magnitude >= enemyHitVelocity)
        {
            damagable.Damage(damageAmount);
        }
    }
}
