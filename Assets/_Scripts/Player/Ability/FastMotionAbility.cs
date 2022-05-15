using UnityEngine;

public class FastMotionAbility : BirdAbility
{
    [Header("Ability Info")]
    [SerializeField] private float speedMultiplier = 2;

    protected override void UseAbility()
    {
        base.UseAbility();
        rb.velocity *= speedMultiplier;
        ChangeSpriteForAnimation(birdPowerUpSprite);
    }
}
