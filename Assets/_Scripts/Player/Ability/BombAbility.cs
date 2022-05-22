using UnityEngine;

public class BombAbility : BirdAbility
{
    [Header("Bomb Bird Info")]
    [SerializeField] private float timeToBlast = 1;
    [SerializeField] private float radius = 0.3f;
    [SerializeField] private float forceMultiplier = 3;
    [SerializeField] private bool isCollided;

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
