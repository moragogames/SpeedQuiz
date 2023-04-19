
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public partial class ColorEffect : MonoBehaviour  // Data Field
{
    private bool isActive = false;
    private float deltaTime = 0;
    private Color lerpSourseColor;
    private Color lerpDestColor;
    [SerializeField] private bool isStartActive = false;
    [SerializeField] private Image targetImage;
    [SerializeField] private AnimationCurve colorAnimationCurve;
    [SerializeField] private Color originColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private float effectTime = 1.0f;
    [SerializeField] private UnityEvent activeEvent;
    [SerializeField] private UnityEvent deactiveEvent;
}
public partial class ColorEffect : MonoBehaviour  // Main
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

        targetImage.color = Color.Lerp(lerpSourseColor, lerpDestColor, colorAnimationCurve.Evaluate(deltaTime));

        if (deltaTime >= 1.0f)
        {
            Deactive();
        }
    }
}

public partial class ColorEffect : MonoBehaviour  // Propery
{
    public void Active()
    {
        isActive = true;
        deltaTime = 0;
        lerpSourseColor = originColor;
        lerpDestColor = targetColor;

        activeEvent?.Invoke();
    }

    public void Deactive()
    {
        isActive = false;
        deactiveEvent?.Invoke();
    }
}




