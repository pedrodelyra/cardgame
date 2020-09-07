using UnityEngine;

namespace Gameplay.Core
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] Transform cornerLeft;
        [SerializeField] Transform cornerRight;

        public Transform GetCorner(Team team) => team == Team.Home
            ? cornerLeft
            : cornerRight;
    }
}
