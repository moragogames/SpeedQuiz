using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDisable : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        gameObject.SetActive(false);
    }


}
