using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Core
{
    public class GestureRecognizer : MonoBehaviour
    {
        enum ProcessingState
        {
            Idle = -1,
            Unknown,
            Swipe
        }

        enum MouseButtonState
        {
            Released,
            Pressed
        }

        int? fingerId;
        Vector2 startPosition;
        ProcessingState processingState;

        Vector2 previousPosition;
        MouseButtonState mouseButtonState;

        float gestureStartTime;
        float lastMouseTime;

        public event Action<Vector2> InputBegan;
        public event Action<Vector2, Vector2, float> InputMoved;
        public event Action<Vector2, Vector2, float> InputHold;
        public event Action<Vector2, Vector2, float> InputEnded;
        public event Action<Vector2> Tapped;

        [SerializeField] float swipeFactor = 40f;
        [SerializeField] float tapDurationThreshold = 0.1f;

        float swipeMovementThreshold;

        void Awake()
        {
            fingerId = null;
            processingState = ProcessingState.Idle;
            mouseButtonState = MouseButtonState.Released;

            swipeMovementThreshold = (Screen.height * swipeFactor) / 1334f;
        }

        void Update()
        {
#if !UNITY_EDITOR
            bool isMobile =
     Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
            if (isMobile) ProcessTouch();
            else ProcessMouse();
#else
            ProcessMouse();
#endif
        }

        void ProcessTouch()
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && null == fingerId)
                {
                    fingerId = touch.fingerId;
                    ProcessTouchBegan(touch.position);
                }

                if (touch.fingerId == fingerId)
                {
                    if (TouchPhase.Moved == touch.phase) ProcessTouchMoved(touch.position, touch.deltaTime);
                    else if (TouchPhase.Ended == touch.phase) ProcessTouchEnded(touch.position, touch.deltaTime);
                    else if (TouchPhase.Stationary == touch.phase) ProcessTouchStationary(touch.position, touch.deltaTime);
                }
            }
        }

        void ProcessMouse()
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (Input.GetMouseButton(0))
            {
                if (mouseButtonState == MouseButtonState.Released) ProcessTouchBegan(mousePosition);
                else if (previousPosition != mousePosition) ProcessTouchMoved(mousePosition, Time.time - lastMouseTime);
                else ProcessTouchStationary(mousePosition, Time.time - lastMouseTime);
                mouseButtonState = MouseButtonState.Pressed;
                previousPosition = mousePosition;
                lastMouseTime = Time.time;
            }
            else if (mouseButtonState == MouseButtonState.Pressed)
            {
                mouseButtonState = MouseButtonState.Released;
                ProcessTouchEnded(mousePosition, Time.time - lastMouseTime);
                lastMouseTime = Time.time;
            }
        }

        bool WithinTapDurationThreshold()
        {
            return (gestureStartTime + tapDurationThreshold) > Time.time;
        }

        bool WithinSwipeMovementThreshold(Vector2 position)
        {
            return (position - startPosition).magnitude > swipeMovementThreshold;
        }

        void ProcessTouchBegan(Vector2 position)
        {
            if (!UIClick(position))
            {
                gestureStartTime = Time.time;
                startPosition = position;
                processingState = ProcessingState.Unknown;
            }
            else
            {
                fingerId = null;
                mouseButtonState = MouseButtonState.Released;
            }
        }

        void ProcessTouchMoved(Vector2 position, float deltaTime)
        {
            Debug.Log($"PROCESS TOUCH MOVED! {processingState.ToString()}");
            if (processingState != ProcessingState.Idle)
            {
                UpdateStateOnTouchMoved(position);

                if (processingState == ProcessingState.Swipe)
                {
                    InputMoved?.Invoke(startPosition, position, deltaTime);
                }
            }
        }

        void ProcessTouchStationary(Vector2 position, float deltaTime)
        {
            if (CanStartSwipe(position))
            {
                InputBegan?.Invoke(startPosition);

                processingState = ProcessingState.Swipe;
            }
            else if (processingState == ProcessingState.Swipe)
            {
                InputHold?.Invoke(startPosition, position, deltaTime);
            }
        }

        bool CanStartSwipe(Vector2 position) => processingState == ProcessingState.Unknown && !WithinTapDurationThreshold()
                                                                                           && WithinSwipeMovementThreshold(position);

        void ProcessTouchEnded(Vector2 position, float deltaTime)
        {
            if (processingState != ProcessingState.Idle)
            {
                UpdateStateOnTouchMoved(position);
                fingerId = null;
                if (processingState == ProcessingState.Unknown)
                {
                    Tapped?.Invoke(position);
                }
                else if (processingState == ProcessingState.Swipe)
                {
                    InputEnded?.Invoke(startPosition, position, deltaTime);
                }

                processingState = ProcessingState.Idle;
            }
        }

        void UpdateStateOnTouchMoved(Vector2 position)
        {
            if (CanStartSwipe(position))
            {
                InputBegan?.Invoke(startPosition);

                processingState = ProcessingState.Swipe;
            }
        }

        bool UIClick(Vector2 position)
        {
            if (EventSystem.current == null)
                return false;

            PointerEventData pe = new PointerEventData(EventSystem.current) { position = position };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pe, results);

            return results.Any(r => r.gameObject && r.gameObject.layer == LayerMask.NameToLayer("UI"))
                       && results.All(r => !r.gameObject || r.gameObject.layer != LayerMask.NameToLayer("RelayOverlay"));
        }
    }
}
