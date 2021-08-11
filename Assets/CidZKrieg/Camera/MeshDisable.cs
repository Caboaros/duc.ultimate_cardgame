using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDisable : MonoBehaviour
{
    MeshRenderer mesh;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }
}
