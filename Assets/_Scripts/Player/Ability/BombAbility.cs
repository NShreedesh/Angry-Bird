using System.Collections;
using UnityEngine;

public class BombAbility : BirdAbility
{
    [Header("Bomb Bird Info")]
    [SerializeField] private float timeToBlast = 1;
    [SerializeField] private float radius = 0.3f;
    [SerializeField] private float forceMultiplier = 3;
    [SerializeField] private bool isCollided;

    [Header("Camera Shake Info")]
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float magnitude = 0.15f;
    private CameraShake cameraShake;

    [Header("Particle Effect")]
    [SerializeField] private ParticleSystem bombBlastParticleEffect;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCollided) return;

        base.OnCollisionEnter2D(collision);
        isCollided = true;

        UseAbility();
    }

    protected override void UseAbility()
    {
        if (isCollided)
        {
            Invoke(nameof(Blast), timeToBlast);
        }
        else
        {
            Blast();
            isCollided = true;
        }

        base.UseAbility();
    }

    private void Blast()
    {
        Collider2D[] collison = Physics2D.OverlapCircleAll(transform.position, radius);


        foreach (Collider2D col in collison)
        {
            if (col.TryGetComponent<IBombable>(out IBombable bombable))
            {
                Vector2 distance = col.transform.position - transform.position;

                distance.x = Mathf.Sign(distance.x) == 1 ? 1 : -1;
                distance.y = Mathf.Sign(distance.y) == 1 ? 1 : -1;

                col.attachedRigidbody.AddForce(distance * forceMultiplier, ForceMode2D.Impulse);
            }
        }
        StartCoroutine(cameraShake.Shake(duration, magnitude));
        Destroy(gameObject, duration);

        StartCoroutine(PlayParticleSystem());
    }

    private IEnumerator PlayParticleSystem()
    {
        yield return new WaitForSeconds((int)duration * 1000);
        ParticleSystem blastParticle = Instantiate(bombBlastParticleEffect, transform.position, transform.rotation);
        blastParticle.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }
}
