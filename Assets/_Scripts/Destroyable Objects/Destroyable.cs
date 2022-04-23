using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
    [Header("Particles Info")]
    [SerializeField] private ParticleSystem destroyParticle;

    [Header("Damage Info")]
    public int damageToEnemy = 1;
    public float damageVelocity = 1;
    public int DamageToEnemy { get { return damageToEnemy; } }
    public float DamageVelocity { get { return damageVelocity; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D collisionFirstContact;

        if (collision.transform.TryGetComponent<Bird>(out Bird bird))
        {
            collisionFirstContact = collision.GetContact(0);

            if (collision.relativeVelocity.magnitude < bird.damageVelocity) return;
            if (collisionFirstContact.normal.y > 0.4f) return;
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        ParticleSystem particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        particle.Stop();
        particle.Play();
        Destroy(gameObject);
    }
}
