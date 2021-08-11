using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy2 : MonoBehaviour
{
    public static Energy2 Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public int[] energy;

    //public int[] slotEnergy = new int[6];

    public int startEnergy;

    public int standartSlotValue;

    private void Start()
    {
        energy[0] = startEnergy;
        energy[1] = startEnergy;

        foreach(GameObject zone in NeoSlot.Instance.zoneList)
        {
            zone.GetComponent<ZoneScript>().currentGain = startEnergy;
        }
    }

    public void CheckSlot(GameObject zone)
    {
        ZoneScript zScript = zone.GetComponent<ZoneScript>();

        if (zScript.state == ZoneState2.Empty)
        {
            zScript.currentGain = standartSlotValue;
        }

        else if (zScript.state == ZoneState2.Filled)
        {
            zScript.currentGain = zScript.cardOnZone.GetComponent<CardScript2>().mana;
        }

        ContabilizeSlot(zone);
    }

    public void ContabilizeSlot(GameObject zone)
    {
        ZoneScript zScript = zone.GetComponent<ZoneScript>();

        energy[(int)zScript.myOwner] += zScript.currentGain;

        // animação
        zScript.ShowText(true, zScript.currentGain);
    }

    public void SubtractEnergy(int ID, int amount)
    {
        energy[ID] -= amount;
    }

    public void ResetSlot(GameObject zone)
    {
        zone.GetComponent<ZoneScript>().currentGain = standartSlotValue;
    }
}
