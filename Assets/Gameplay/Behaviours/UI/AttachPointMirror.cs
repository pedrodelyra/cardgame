using UnityEngine;
using UnityEngine.Assertions;

namespace Gameplay.Behaviours.UI
{
    public class AttachPointMirror : MonoBehaviour
    {
        [SerializeField] Transform root;
        [SerializeField] RectTransform innerContainer;
        [SerializeField] Vector2 offset;
        [SerializeField] bool followOnUpdate;

        Transform AttachPoint { get; set; }

        Camera _camera;

        void Awake()
        {
            _camera = Camera.main;
        }

        void Start()
        {
            Assert.IsNotNull(AttachPoint, "AttachPoint should not be null");
        }

        void Update()
        {
            if (!followOnUpdate)
            {
                return;
            }

            // Let's move our UI element to AttachPoint's position
            root.position = _camera.WorldToScreenPoint(AttachPoint.position);
            innerContainer.anchoredPosition = offset;
        }

        public void Setup(Transform attachPoint)
        {
            AttachPoint = attachPoint;
        }
    }
}
