using UnityEngine;

public class FastMotionAbility : BirdAbility
{
    [SerializeField] private float speedMultiplier = 2;

    protected override void UseAbility()
    {
        base.UseAbility();
        rb.velocity *= speedMultiplier;
        ChangeSpriteForAnimation(birdPowerUpSprite);
    }
}
