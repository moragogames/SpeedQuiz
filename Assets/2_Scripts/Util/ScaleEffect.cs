/*
 * Coder        : Coder
 * LastUpdate   : 2023. 01. 26
 * Information  : ScaleEffect
 */
namespace RapidFramework 
{ 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class ScaleEffect : MonoBehaviour  // Date Field
    {
        private bool isActive = false;
        private float deltaTime = 0;
        private Vector3 lerpSourseScale;
        private Vector3 lerpDestScale;
        [SerializeField] private bool isStartActive = false;
        [SerializeField] private bool isLoop = false;
        [SerializeField] private Transform target;
        [SerializeField] private AnimationCurve scaleAnimationCurve;
        [SerializeField] private Vector3 originScale;
        [SerializeField] private Vector3 targetScale;
        [SerializeField] private float effectTime = 1.0f;
        [SerializeField] private UnityEvent activeEvent;
        [SerializeField] private UnityEvent deactiveEvent;
    }
    public partial class ScaleEffect : MonoBehaviour  // Main
    {
        private void Start()
        {
            if(isStartActive == true)
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

            target.localScale = Vector3.Lerp(lerpSourseScale, lerpDestScale, scaleAnimationCurve.Evaluate(deltaTime));

            if (deltaTime >= 1.0f)
            {
                if(isLoop == true)
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

    public partial class ScaleEffect : MonoBehaviour  // propery
    {
        public void Active()
        {
            isActive= true;
            deltaTime = 0;
            lerpSourseScale = originScale;
            lerpDestScale = targetScale;

            activeEvent?.Invoke();
        }

        public void Deactive()
        {
            isActive = false;
            deactiveEvent?.Invoke();
        }
    }

}
