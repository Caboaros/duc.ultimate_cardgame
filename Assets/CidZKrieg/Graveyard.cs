using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    public static Graveyard Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    [System.Serializable]
    public class Lists
    {
        public List<GameObject> cardList;
    }

    public Lists[] graveLists;

    public void TriggerAnimation()
    {
        CardManager.Instance.selectedCard.GetComponent<CardScript>().visualsAnim.SetTrigger("Grave");
    }

    public void AddToGrave(GameObject card, int ID)
    {
        graveLists[ID].cardList.Add(card);
        ArrangeGrave(ID);
    }

    public void ArrangeGrave(int ID)
    {
        float index = 0;

        foreach (GameObject card in graveLists[ID].cardList)
        {
            index++;
            card.transform.position = new Vector3(Board.Instance.graveHolder[ID].transform.position.x, (float)Board.Instance.graveHolder[ID].transform.position.y + (index / 50), Board.Instance.graveHolder[ID].transform.position.z);
        }
    }
}
