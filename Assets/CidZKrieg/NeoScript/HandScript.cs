using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    // Singleton

    public static HandScript Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    //

    public GameObject emptyPrefab;

    public Transform[] center;

    [System.Serializable]
    public class HandLists
    {
        public List<GameObject> emptyItens;
        public List<GameObject> cardsOnHand;
    }

    public HandLists[] handLists;

    public void LateUpdate()
    {
        ArrangeHand(0);
        ArrangeHand(1);
    }

    public void AddItem(int ID)
    {
        GameObject tmpItem = Instantiate(emptyPrefab, center[ID].position, center[ID].rotation);
        handLists[ID].emptyItens.Add(tmpItem);
        tmpItem.transform.SetParent(Board2.Instance.prefabStorage.transform);
        UpdateCardIndex(ID);
    }

    public void DestroyItem(int i, int ID)
    {
        Destroy(handLists[ID].emptyItens[i]);
        handLists[ID].emptyItens.Remove(handLists[ID].emptyItens[i]);
    }

    public void AddCardToHand(GameObject card, int ID)
    {
        handLists[ID].cardsOnHand.Add(card);
        AddItem(ID);
        ArrangeGrid(ID);
    }

    public void ArrangeGrid(int ID)
    {
        if (handLists[ID].emptyItens.Count > 1)
        {
            handLists[ID].emptyItens[0].transform.position = new Vector3(-0.5f * (handLists[ID].emptyItens.Count - 1), center[ID].position.y, center[ID].position.z);

            for (int i = 1; i < handLists[ID].emptyItens.Count; i++)
            {
                handLists[ID].emptyItens[i].transform.position = new Vector3((handLists[ID].emptyItens[0].transform.position.x + i), center[ID].position.y, center[ID].position.z);
            }
        }

        else if (handLists[ID].emptyItens.Count == 1)
        {
            handLists[ID].emptyItens[0].transform.position = center[ID].position;
        }

    }

    public void UpdateCardIndex(int ID)
    {
        for (int i = 0; i < handLists[ID].cardsOnHand.Count; i ++)
        {
            handLists[ID].cardsOnHand[i].GetComponent<CardScript2>().myIndex = i;
        }
    }

    public void ArrangeHand(int ID)
    {
        if (handLists[ID].cardsOnHand.Count > 1)
        {
            for (int i = 0; i < handLists[ID].cardsOnHand.Count; i++)
            {
                Vector3 slotTransform = new Vector3(handLists[ID].emptyItens[i].transform.position.x, handLists[ID].emptyItens[i].transform.position.y - ((float)i / 100), handLists[ID].emptyItens[i].transform.position.z);
                handLists[ID].cardsOnHand[i].transform.position = Vector3.Lerp(handLists[ID].cardsOnHand[i].transform.position, slotTransform, 5 * Time.deltaTime);
            }
        }

        else if (handLists[ID].cardsOnHand.Count == 1)
        {
            handLists[ID].cardsOnHand[0].transform.position = Vector3.Lerp(handLists[ID].cardsOnHand[0].transform.position, center[ID].transform.position, 5 * Time.deltaTime);
        }
    }

    public void RemoveFromHand(int ID, GameObject card)
    {
        handLists[ID].cardsOnHand.Remove(card);
        DestroyItem(card.GetComponent<CardScript2>().myIndex, ID);
        ArrangeGrid(ID);
        UpdateCardIndex(ID);
    }

}

