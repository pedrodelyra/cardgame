using Gameplay.Behaviours.Interfaces;
using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours.UI
{
    public class GameplayHUD : MonoBehaviour
    {
        [SerializeField] RectTransform containerHUD;

        [Header("Prefabs")]
        [SerializeField] TroopHealthBar prefabTroopHealthBar;

        public void CreateHealthBar(IDamageable damageable, Team team, Transform attachPoint)
        {
            var healthBar = Instantiate(prefabTroopHealthBar, containerHUD);
            healthBar.Setup(damageable, team, attachPoint);
        }
    }
}
