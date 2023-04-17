    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;

    public partial class PositionEffect : MonoBehaviour // Data Field
    {
        private bool isActive = false;
        private float deltaTime = 0;
        private Vector3 lerpSoursePosition;
        private Vector3 lerpDestPosition;
        [SerializeField] private bool isStartActive = false;
        [SerializeField] private bool isLoop = false;
        [SerializeField] private Transform target;
        [SerializeField] private AnimationCurve positionAnimationCuve;
        [SerializeField] private Vector3 originPosition;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private float effectTime = 1.0f;
        [SerializeField] private UnityEvent activeEvent;
        [SerializeField] private UnityEvent deactiveEvent;
    }
    public partial class PositionEffect : MonoBehaviour // Main
    {
        private void Start()
        {
            if (isStartActive == true)
            {
                Active();
            }
        }
        private void Update()
        {
            if (isActive)
            {
                Progress();
            }
        }

        public void Progress()
        {
            deltaTime += Time.deltaTime / effectTime;

            target.localPosition = Vector3.Lerp(lerpSoursePosition, lerpDestPosition, positionAnimationCuve.Evaluate(deltaTime));

            if (deltaTime >= 1.0f)
            {
                if (isLoop == true)
                {
                    deltaTime = 0;
                }
                else
                {
                    Deactive();
                }
            }
        }
    }
    public partial class PositionEffect : MonoBehaviour // Property
    {
        public void Active()
        {
            isActive= true;
            deltaTime= 0;
            lerpSoursePosition = originPosition;
            lerpDestPosition = targetPosition;

            activeEvent.Invoke();
        }

        public void Deactive()
        {
            isActive = false;
            deactiveEvent.Invoke();
        }
    }


