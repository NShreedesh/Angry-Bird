public interface IDamagable
{
    public int Health { get; }
    public void Damage(int damageAmount);
}
