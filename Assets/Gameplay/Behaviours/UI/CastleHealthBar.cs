using Gameplay.Behaviours.Interfaces;
using Gameplay.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gameplay.Behaviours.UI
{
    public class CastleHealthBar : MonoBehaviour
    {
        [SerializeField] Entity castle;
        [SerializeField] HealthBar healthBar;

        void Start()
        {
            Assert.IsNotNull(castle, "castle should not be null");
            Assert.IsNotNull(healthBar, "healthBar should not be null");
            var damageable = castle.GetComponent<IDamageable>();
            healthBar.Setup(damageable, castle.Team);
        }
    }
}
