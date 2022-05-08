using System.Threading.Tasks;

public interface IDamagable
{
    public int Health { get; }
    public void Damage(int damageAmount);
}
