using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void PlayParticule(ParticleSystem particleSystem)
    {
        particleSystem.Play();
    }

}
