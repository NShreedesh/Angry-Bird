using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [Header("Particles Info")]
    [SerializeField] private ParticleSystem destroyParticle;


    [Header("Damage Info")]
    public int damageToEnemy = 1;
    public float damageVelocity = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Bird>(out Bird bird))
        {
            if (collision.relativeVelocity.magnitude < bird.damageVelocity) return;
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
