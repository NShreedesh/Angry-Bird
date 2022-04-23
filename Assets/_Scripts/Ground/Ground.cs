using UnityEngine;

public class Ground : MonoBehaviour, IDamagable
{
    public int damageToEnemy = 1;
    public float damageVelocity = 3;

    public int DamageToEnemy { get { return damageToEnemy; } }
    public float DamageVelocity { get { return damageVelocity; } }
}
