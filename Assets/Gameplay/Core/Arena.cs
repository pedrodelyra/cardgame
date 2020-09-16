using Gameplay.Behaviours;
using UnityEngine;

namespace Gameplay.Core
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] Lane[] lanes; // TODO: Use lanes

        public Lane[] Lanes => lanes;
    }
}
