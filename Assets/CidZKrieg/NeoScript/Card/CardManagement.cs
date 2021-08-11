using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardManagement : MonoBehaviour
{
    // Futuramente colocar aqui as funcionalidades envolvendo cartas

    public List<GameObject> awaitingList = new List<GameObject>();

    public UnityEvent SummonEvent;

    public void AddToList()
    {
        awaitingList.Add(GameManager.Instance.selectedCard);
    }

    

}