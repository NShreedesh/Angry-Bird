using UnityEngine;

public class Hit : MonoBehaviour
{
    [Header("Component Info")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Enemy Damage Info")]
    [Range(1, 3)]
    [SerializeField] private int damageAmount;
    private bool hasHit;

    [Header("Values For Destroyables")]
    [SerializeField] private float minVelocityToDestroy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Mathf.Sign(collision.GetContact(0).normal.y) == -1) return;

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
        if (rb.velocity.magnitude > minVelocityToDestroy)
        {
            if (collision.transform.TryGetComponent<IDestroyable>(out IDestroyable destroyable))
            {
                destroyable.DestoryObject();
            }
        }
    }
}
