using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Texture2D pointerTexture;
    public Texture2D selectTexture;
    public Texture2D handTexture;

    public LayerMask layerMask;

    public Color outlineColor;

    Renderer render;

    private void Start()
    {
        Cursor.SetCursor(pointerTexture, new Vector2(0, 0), CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;

    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            // Mudando cursor
            Cursor.SetCursor(selectTexture, new Vector2(0, 0), CursorMode.Auto);

            // Outline

            if (render != null)
            {
                render.material.SetFloat("_OutlineWidth", 0f);
            }
            // Essa parte acima serve pra que um objeto não se mantenha em destaque caso haja colisão de dois objetos interagíveis ao mesmo tempo

            if (hit.collider.tag == "Card")
            {
                render = hit.collider.GetComponent<CardScript2>().outlineRenderer;
            }

            else
            {
                render = hit.collider.gameObject.GetComponent<Renderer>();
            }

            if (render.material.shader.name == "Outlined/Uniform" || render.material.shader.name == "Outlined/Silhouette Only")
            {

                if (render.material.GetColor("_OutlineColor") != outlineColor)
                {
                    render.material.SetColor("_OutlineColor", outlineColor);
                }

                render.material.SetFloat("_OutlineWidth", 0.03f);

            }
        }

        else
        {
            if (render != null)
            {
                render.material.SetFloat("_OutlineWidth", 0f);
            }
            Cursor.SetCursor(pointerTexture, new Vector2(0, 0), CursorMode.Auto);
        }

        if (Input.GetButton("Fire2"))
        {
            Cursor.SetCursor(handTexture, new Vector2(0, 0), CursorMode.Auto);
        }
    }
}
