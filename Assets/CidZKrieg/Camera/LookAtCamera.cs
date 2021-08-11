using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Camera.main.enabled)
            {
                transform.LookAt(Camera.main.transform);
            }

        }
    }
}
