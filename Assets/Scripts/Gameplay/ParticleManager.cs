using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public GameObject _hitParticle;
    public GameObject _earnXPParticle;
    public GameObject _smokeParticle;
    private void Awake()
    {
        instance = this;
    }

    public void PlayParticule(ParticleSystem particleSystem)
    {
        particleSystem.Play();
    }

}
