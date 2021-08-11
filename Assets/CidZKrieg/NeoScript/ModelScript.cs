using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour
{
    public Renderer mRenderer;

    public void ChangeMaterial(Material mat)
    {
        mRenderer.material = mat;
    }
}
