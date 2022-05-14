using UnityEngine;

public class BombAbility : BirdAbility
{
    protected override void UseAbility()
    {
        base.UseAbility();
        print("Bomb Ability");
    }
}
