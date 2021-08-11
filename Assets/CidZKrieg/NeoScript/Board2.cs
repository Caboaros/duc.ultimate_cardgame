using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board2 : MonoBehaviour
{
    //Singleton
    public static Board2 Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public GameObject[] deckHolder;
    public GameObject[] handHolder;
    public GameObject[] graveHolder;

    //Objeto que serve de parent para prefabs vazios, para evitar bagunça no hierarchy
    public GameObject prefabStorage;

    private void Start()
    {
        
    }
}
