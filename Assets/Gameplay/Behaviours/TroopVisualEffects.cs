using UnityEngine;

namespace Gameplay.Behaviours
{
    public class TroopVisualEffects : MonoBehaviour
    {
        [SerializeField] ParticleSystem hitParticle;

        public void PlayHitParticle()
        {
            Destroy(Instantiate(hitParticle, transform), 10);
        }
    }
}