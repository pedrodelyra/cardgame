using Gameplay.Behaviours.Interfaces;
using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours.UI
{
    public class TroopHealthBar : MonoBehaviour
    {
        [SerializeField] HealthBar healthBar;
        [SerializeField] AttachPointMirror attachPointMirror;

        public void Setup(IDamageable damageable, Team team, Transform attachPoint)
        {
            healthBar.Setup(damageable, team);
            attachPointMirror.Setup(attachPoint);
            damageable.OnDie += OnDamageableDied;
        }

        void OnDamageableDied(IDamageable damageable)
        {
            damageable.OnDie -= OnDamageableDied;
            Destroy(gameObject);
        }
    }
}
    