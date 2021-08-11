using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidesTransitate : MonoBehaviour
{
    public Animator cameraHolder;

    public void Transitate()
    {
        if (cameraHolder.GetBool("Angle"))
        {
            cameraHolder.SetBool("Angle", false);
        }

        else
        {
            cameraHolder.SetBool("Angle", true);
        }
    }
}
