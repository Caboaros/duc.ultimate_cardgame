using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : MonoBehaviour
{
    public static Zones Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }



    // === 

    // Classe contendo atributos das zonas
    [System.Serializable]
    public class Zone
    {
        // Criando os componentes de cada Zona
        public GameObject zone;
        public ZoneState zoneState;
        public Card cardOnZone;

    }

    //Classe que cria Listas da classe acima
    [System.Serializable]
    public class ZoneList
    {
        public List<Zone> zoneObj = new List<Zone>();
    }

    // ===



    public ZoneList[] zoneList;

    public int zoneIndex;

    public bool canInvoke;

    // Checando estados das Zonas, para saber quais estão disponíveis
    public void ZoneCheck(int ID)
    {
        for (int i = zoneList[ID].zoneObj.Count -1; i > -1; i --)
        {
            if (zoneList[ID].zoneObj[i].zoneState == ZoneState.Empty)
            {
                zoneIndex = i;
            }

        }
    }

    public void CheckState(int ID)
    {
        foreach(Zone z in zoneList[ID].zoneObj)
        {
            if (z.zoneState == ZoneState.Empty)
            {
                canInvoke = true;
            }

            else
            {
                canInvoke = false;
            }
        }
    }

    public void ChangeState(int ID, int i, ZoneState zState)
    {
        zoneList[ID].zoneObj[i].zoneState = zState;
    }
}

// Estados das zonas
public enum ZoneState
{
    Empty,
    Filled,
}

