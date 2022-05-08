
using System.Threading.Tasks;

public interface IDestroyable
{
    public int Health { get; }
    public void Destroy();
}
