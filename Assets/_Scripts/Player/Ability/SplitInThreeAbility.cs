using UnityEngine;

public class SplitInThreeAbility : BirdAbility
{
    [Header("Split Bird Info")]
    [SerializeField] private GameObject splitBird;
    [SerializeField] private float xForceAmount = 2;
    [SerializeField] private float yForceAmount = 2;

    protected override void UseAbility()
    {
        base.UseAbility();

        SplitBird(xForceAmount, -yForceAmount);
        SplitBird(xForceAmount, yForceAmount);
        rb.AddForce(new Vector2(xForceAmount, 0), ForceMode2D.Impulse);
    }

    private void SplitBird(float xForce, float yForce)
    {
        GameObject splitBird = Instantiate(this.splitBird, transform.position, Quaternion.identity);
        Bird bird = splitBird.GetComponent<Bird>();
        Rigidbody2D splitBirdRb = splitBird.GetComponent<Rigidbody2D>();

        splitBirdRb.isKinematic = false;
        bird.isLaunched = true;

        splitBirdRb.velocity = rb.velocity;
        splitBirdRb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }
}
