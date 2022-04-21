using UnityEngine;
using UnityEngine.Pool;

public class WoodObject : MonoBehaviour, IDestroyable
{
    public void DestoryObject(ParticleSystem particleSystem)
    {
        particleSystem.Stop();
        particleSystem.Play();
        Destroy(gameObject);
    }
}
