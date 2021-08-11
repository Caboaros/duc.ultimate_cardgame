using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NeoSlot : MonoBehaviour
{
    [Header("O adjacente 0 é o anterior, o 1 é o posterior")]

    // Singleton
    public static NeoSlot Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public Material dayColor;
    public Material nightColor;
    public Material blueColor;

    public Mesh sunModel, moonModel;
    public Color sunColor, moonColor, selectedColor;

    public int midDay;
    public int midNight;

    public List<GameObject> zoneList = new List<GameObject>();

    public RotationType Orientation;

    public GameObject selectedTower;

    private void Start()
    {
        ZoneCheck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RotateSymbols());
        }
    }

    public void ZoneRotation()
    {
        EventIntervals.Instance.CheckTransition(true);

        if (Orientation == RotationType.Normal)
        {
            //day
            if (midDay != 5)
            {
                midDay += 1;
            }

            else
            {
                midDay = 0;
            }

            //night
            if (midNight != 5)
            {
                midNight += 1;
            }

            else
            {
                midNight = 0;
            }
        }

        else
        {
            //day
            if (midDay != 0)
            {
                midDay -= 1;
            }

            else
            {
                midDay = 5;
            }

            //night
            if (midNight != 0)
            {
                midNight -= 1;
            }

            else
            {
                midNight = 5;
            }
        }
        

        ZoneCheck();

        StartCoroutine(RotateSymbols());
    }

    // Muda o estado dos slots
    // Os slots correspondentes a "midDay" e "midDay" são mudados pra dia e noite respectivamente
    // Os slots adjacentes dos slots medianos são mudados para o mesmo estado

    public void ZoneCheck()
    {
        ChangeSide(zoneList[midDay], Side.Day);
        for (int i = 0; i < 2; i++)
        {
            ChangeSide(zoneList[midDay].GetComponent<ZoneScript>().adjacents[i], Side.Day);

        }

        ChangeSide(zoneList[midNight], Side.Night);
        for (int i = 0; i < 2; i++)
        {
            ChangeSide(zoneList[midNight].GetComponent<ZoneScript>().adjacents[i], Side.Night);
        }

        ChangeSlotMaterial();
    }

    public void ChangeSide(GameObject zone, Side side)
    {
        zone.GetComponent<ZoneScript>().currentSide = side;
        zone.GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().ChangeSide(side);
    }

    // Removendo carta do slot
    public void RemoveFromSlot(int ID, GameObject card)
    {
        zoneList[ID].GetComponent<ZoneScript>();
    }

    // Mudando estado do slot
    public void ChangeZoneState(ZoneState2 s, int zoneID)
    {
        zoneList[zoneID].GetComponent<ZoneScript>().state = s;
        CheckState(zoneID);
    }

    // "Limpando" a torre
    public void ClearZone(int zoneID)
    {
        zoneList[zoneID].GetComponent<ZoneScript>().Clear();
    }

    //
    public void CheckState(int zoneID)
    {
        if (zoneList[zoneID].GetComponent<ZoneScript>().state == ZoneState2.Empty)
        {
            zoneList[zoneID].GetComponent<ZoneScript>().cardOnZone = null;
        }
    }

    public void ChangeSlotMaterial()
    {
        foreach (GameObject zone in zoneList)
        {
            // "Highlight" faz com que o slot fique em destaque
            zone.GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().SetHighLight(false);

            // Cilindro (Apagar depois)
            if (zone.GetComponent<ZoneScript>().currentSide == Side.Day)
            {
                zone.GetComponent<MeshRenderer>().material = dayColor;
            }

            else if (zone.GetComponent<ZoneScript>().currentSide == Side.Night)
            {
                zone.GetComponent<MeshRenderer>().material = nightColor;
            }
            //
        }

        // Cilindro selecionado
        zoneList[TurnActions.Instance.currentSlot].GetComponent<MeshRenderer>().material = blueColor;
        //

        zoneList[TurnActions.Instance.currentSlot].GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().SetHighLight(true);

    }

    public void InvokeVanish()
    {
        StartCoroutine(VanishSymbols());
    }

    public void InvokeAppear()
    {
        StartCoroutine(AppearSymbols());
    }

    IEnumerator RotateSymbols()
    {
        print("RotateSymbols");

        AudioManager.Instance.SFX.PlayTowerSFX("Twinkle");

        if (Orientation == RotationType.Normal)
        {
            for (int i = 0; i < zoneList.Count; i++)
            {
                yield return new WaitForSeconds(0.1f);
                zoneList[i].GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().anim.SetTrigger("Change");
            }
        }

        else if (Orientation == RotationType.Reverse)
        {
            for (int i = zoneList.Count; i > 0; i--)
            {
                yield return new WaitForSeconds(0.1f);
                zoneList[i].GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().anim.SetTrigger("Change");
            }
        }

        EventIntervals.Instance.CheckTransition(false);

    }

    IEnumerator VanishSymbols()
    {
        print("VanishSymbols");

        if (Orientation == RotationType.Normal)
        {
            for (int i = 0; i < zoneList.Count; i++)
            {
                yield return new WaitForSeconds(0.1f);
                zoneList[i].GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().anim.SetTrigger("Vanish");
            }
        }

        else if (Orientation == RotationType.Reverse)
        {
            for (int i = zoneList.Count; i > 0; i--)
            {
                yield return new WaitForSeconds(0.1f);
                zoneList[i].GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().anim.SetTrigger("Vanish");
            }
        }
    }

    IEnumerator AppearSymbols()
    {
        print("AppearSymbols");

        yield return new WaitForSeconds(2f);

        if (Orientation == RotationType.Normal)
        {
            for (int i = 0; i < zoneList.Count; i++)
            {
                yield return new WaitForSeconds(0.1f);
                zoneList[i].GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().anim.SetTrigger("Appear");
            }
        }

        else if (Orientation == RotationType.Reverse)
        {
            for (int i = zoneList.Count; i > 0; i--)
            {
                yield return new WaitForSeconds(0.1f);
                zoneList[i].GetComponent<ZoneScript>().symbol.GetComponent<ZoneSymbol>().anim.SetTrigger("Appear");
            }
        }
    }
}

public enum RotationType
{
    Normal = 1,
    Reverse = -1,
}

