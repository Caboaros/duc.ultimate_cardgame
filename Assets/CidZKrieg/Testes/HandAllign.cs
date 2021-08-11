using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAllign : MonoBehaviour
{
    // Objeto que marca o centro da mão
    public Transform[] center;

    // Prefab vazio (Placeholder pra posição das cartas)
    public GameObject emptyPrefab;
    
    [System.Serializable]
    public class Lists
    {
        public List<GameObject> listObj;
        public List<GameObject> cardsOnHand;
    }

    public Lists[] handLists;

    // Espaço entre as cartas (Por enquanto inutilizável)
    // public float gap;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //AddItem();
        }

    }

    private void LateUpdate()
    {
        ArrangeHand(0);
        ArrangeHand(1);
    }

    public void AddItem(int ID)
    {
        GameObject tmpObj = Instantiate(emptyPrefab, center[ID].position, center[ID].rotation);
        handLists[ID].listObj.Add(tmpObj);
        tmpObj.transform.SetParent(GetComponent<Transform>());

        ArrangeGrid(ID);
    }

    public void DestroyItem(int i, int ID)
    {
        Destroy(handLists[ID].listObj[i]);
        handLists[ID].listObj.Remove(handLists[ID].listObj[i]);
    }

    public void ArrangeGrid(int ID)
    {

        if (handLists[ID].listObj.Count > 1)
        {
            handLists[ID].listObj[0].transform.position = new Vector3(-0.5f * (handLists[ID].listObj.Count - 1), center[ID].position.y, center[ID].position.z);

            for (int i = 1; i < handLists[ID].listObj.Count; i++)
            {
                handLists[ID].listObj[i].transform.position = new Vector3((handLists[ID].listObj[0].transform.position.x + i), center[ID].position.y, center[ID].position.z);
            }
        }

        else if (handLists[ID].listObj.Count == 1)
        {
            handLists[ID].listObj[0].transform.position = center[ID].position;
        }
        
    }

    public void ArrangeHand(int ID)
    {
        if (handLists[ID].cardsOnHand.Count > 1)
        {
            for (int i = 0; i < handLists[ID].cardsOnHand.Count; i++)
            {
                Vector3 slotTransform = new Vector3(handLists[ID].listObj[i].transform.position.x, handLists[ID].listObj[i].transform.position.y - ((float)i / 100), handLists[ID].listObj[i].transform.position.z);
                handLists[ID].cardsOnHand[i].transform.position = Vector3.Lerp(handLists[ID].cardsOnHand[i].transform.position, slotTransform, 5 * Time.deltaTime);
            }
        }

        else if (handLists[ID].cardsOnHand.Count == 1)
        {
            handLists[ID].cardsOnHand[0].transform.position = Vector3.Lerp(handLists[ID].cardsOnHand[0].transform.position, center[ID].transform.position, 5 * Time.deltaTime);
        }
    }

    public void RemoveFromHand(int ID)
    {
        CardManager.Instance.RemoveHand(handLists[ID].cardsOnHand, CardManager.Instance.selectedCard);
        DestroyItem(CardManager.Instance.selectedCard.GetComponent<CardScript>().myIndex, ID);

        ArrangeGrid(ID);
    }

    public void InvokeFromHand()
    {
        RemoveFromHand((int)Sides.Instance.current);
    }
}
