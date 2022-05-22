using UnityEngine;

public class BirdAbility : MonoBehaviour
{
    [Header("InputAction Info")]
    private InputActions inputActions;

    [Header("Other Scipts Info")]
    [SerializeField] private Bird bird;

    [Header("Physics Info")]
    [SerializeField] protected Rigidbody2D rb;

    [Header("Ability Info")]
    public bool hasUsedAbility;
    public bool canLaunchNextBird;

    [Header("Animation Info")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite birdPowerUpSprite;
    [SerializeField] protected Sprite birdCollidedSprite;

    [Header("Particles Info")]
    [SerializeField] private ParticleSystem birdAbilityParticle;

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();

        inputActions.Player.LeftMouse.started += ctx => GiveAbility();
    }

    private void GiveAbility()
    {
        if (!bird.isLaunched) return;
        if (bird.isDestroyed) return;
        if (hasUsedAbility) return;

        UseAbility();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        hasUsedAbility = true;
        canLaunchNextBird = true;
        ChangeSpriteForAnimation(birdCollidedSprite);
    }

    protected virtual void UseAbility()
    {
        ParticleSystem abilityPartilce = Instantiate(birdAbilityParticle, transform.position, Quaternion.identity);
        abilityPartilce.Play();

        hasUsedAbility = true;
        Invoke(nameof(TimeToLaunchNextBird), 1);
    }

    private void TimeToLaunchNextBird()
    {
        canLaunchNextBird = true;
    }

    protected void ChangeSpriteForAnimation(Sprite spriteToChange)
    {
        if (spriteToChange != null)
        {
            spriteRenderer.sprite = spriteToChange;
        }
    }
}
