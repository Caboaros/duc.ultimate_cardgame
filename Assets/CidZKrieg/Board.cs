using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public GameObject[] deckHolder;
    public GameObject[] graveHolder;
    public GameObject[] Zones;
    public GameObject Hands;
    //public int cardAmount;
}


