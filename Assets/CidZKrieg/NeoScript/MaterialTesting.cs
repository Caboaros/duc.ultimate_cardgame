using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTesting : MonoBehaviour
{
    public static MaterialTesting Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void SetTexture(Material changeMaterial, Sprite spriteTexture)
    {
        Material tmpMaterial = changeMaterial;

        tmpMaterial.mainTexture = spriteTexture.texture;

        changeMaterial = tmpMaterial;
    }

}
