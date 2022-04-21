using UnityEngine;
using UnityEngine.Pool;

public class WoodObject : MonoBehaviour, IDestroyable
{
    [SerializeField] private ParticleSystem woodParticle;

    public void DestoryObject()
    {
        GameObject particle = Instantiate(woodParticle.gameObject, transform.position, Quaternion.identity);
        ParticleSystem particleSystemForWood = particle.GetComponent<ParticleSystem>();

        particleSystemForWood.Stop();
        particleSystemForWood.Play();
        Destroy(gameObject);
    }
}
