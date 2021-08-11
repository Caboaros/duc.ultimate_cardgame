using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public GameObject holder;
    public float speed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            holder.transform.Rotate(0, 45, 0);
        }

        //transform.rotation = Quaternion.LerpUnclamped(transform.rotation.normalized, holder.transform.rotation.normalized, Time.deltaTime * speed);
        LerpY(transform, holder.transform, speed);
    }

    public void LerpY(Transform a, Transform b, float speed)
    {
        if (a.localRotation.y < b.localRotation.y)
        {
            a.Rotate(0, 1 * speed * Time.deltaTime, 0);
            //a.localRotation = Quaternion.Euler(0, a.localRotation.y + 10 * speed * Time.deltaTime, 0);
        }

        else if (a.localRotation.y > b.localRotation.y)
        {
            a.Rotate(0, -1 * speed * Time.deltaTime, 0);
            //a.rotation = Quaternion.Euler(0, a.localRotation.y - 0.5f * speed * Time.deltaTime, 0);
        }
    }
}
