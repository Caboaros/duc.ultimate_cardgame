using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSymbol : MonoBehaviour
{
    // Renderizador
    Renderer render;
    MaterialPropertyBlock pBlock;

    // Tempo de transição entre uma cor e outra
    public float[] lerpTime;

    public int lerpIndex;

    // Cor atual do objeto
    public Color currentColor;

    ParticleSystem pSystem;

    public Animator anim;

    public Side mySide;

    public bool highLight;

    public Light zoneLight;

    private void Start()
    {
        pBlock = new MaterialPropertyBlock();
        render = GetComponent<Renderer>();
        pSystem = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (render.material.color != currentColor)
        {
            // Cor

            render.material.color = Color.Lerp(render.material.color, currentColor, (lerpTime[lerpIndex] * Time.deltaTime));

            // Luz

            zoneLight.color = Color.Lerp(render.material.color, currentColor, (lerpTime[lerpIndex] * Time.deltaTime));

            // Emissão (brilho)

            render.material.EnableKeyword("_EMISSION");

            Color emissionColor = render.material.color * 5f;

            render.material.SetColor("_EmissionColor", emissionColor);
        }

        if (NeoSlot.Instance.Orientation == RotationType.Normal)
        {
            transform.Rotate(new Vector3(0, 0, 2f * Time.deltaTime));
        }
        
        else if (NeoSlot.Instance.Orientation == RotationType.Reverse)
        {
            transform.Rotate(new Vector3(0, 0, -2f * Time.deltaTime));
        }
    }

    public void ChangeSide(Side s)
    {
        mySide = s;
    }

    public void SetHighLight(bool b)
    {
        highLight = b;

        if (!EventIntervals.Instance.transition)
        {
            CheckState();
        }

    }

    // Método usado em animação (Muda visuais)
    public void CheckState()
    {
        var sh = pSystem.shape;

        if (mySide == Side.Day)
        {
            GetComponent<MeshFilter>().mesh = NeoSlot.Instance.sunModel;
            currentColor = NeoSlot.Instance.sunColor;

        

            sh.mesh = NeoSlot.Instance.sunModel;

            //transform
            //transform.localScale = new Vector3(0.25f, 0.25f);
            //

            //pSystem.main.startColor.Equals(NeoSlot.Instance.sunColor);

            //pSystem.shape.mesh = ;
        }

        else if (mySide == Side.Night)
        {
            GetComponent<MeshFilter>().mesh = NeoSlot.Instance.moonModel;
            currentColor = NeoSlot.Instance.moonColor;

            sh.mesh = NeoSlot.Instance.moonModel;

            //transform
            //transform.localScale = new Vector3(0.1f, 0.1f);
            //

            //pSystem.main.startColor.Equals(NeoSlot.Instance.moonColor);
        }

        if (highLight == true)
        {
            currentColor = NeoSlot.Instance.selectedColor;

            ChangeParticleSize(1);
        }

        else
        {
            //   CheckState();

            ChangeParticleSize(0.5f);
        }

        pSystem.startColor = currentColor;

    }

    public void ChangeIndex(int i)
    {
        lerpIndex = i;
    }

    public void ChangeParticleSize(float f)
    {
        pSystem.startSize = f;
    }

}



