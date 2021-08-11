using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cemetery : MonoBehaviour
{

    public static Cemetery Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    [System.Serializable]
    public class GraveList
    {
        public List<GameObject> cards = new List<GameObject>();
        /////public Stack<GameObject> cardStack = new Stack<GameObject>();
    }

    public GraveList[] graveyard;

    public void AddToCemetery(GameObject card, int ID)
    {
        TurnActions.Instance.RemoveCardFromList(card);

        graveyard[ID].cards.Add(card);
        /////graveyard[ID].cardStack.Push(card);
        card.GetComponent<CardScript2>().ChangeCardState(CardState.Grave);

        Players.Instance.playerData[ID].hasDiscarded = true;

        ArrangeCemetery(ID);
    }

    public void RemoveFromCemetery(GameObject card, int ID)
    {
        graveyard[ID].cards.Remove(card);
    }

    public void ArrangeCemetery(int ID)
    {
        float index = 0;

        foreach(GameObject card in graveyard[ID].cards)
        {
            index++;

            CardScript2 cardScript = card.GetComponent<CardScript2>();

            cardScript.ChangeCardState(CardState.Grave);
            cardScript.ChangePosition(0, index / 100, 0);
            cardScript.ChangeRotation(-90, Board2.Instance.deckHolder[(int)cardScript.myOwner].transform.rotation.y, 180);
        }
    }

    public void DiscardEvent()
    {
        AddToCemetery(GameManager.Instance.selectedCard, (int)GameManager.Instance.selectedCard.GetComponent<CardScript2>().myOwner);
        if (TurnActions.Instance.currentSlot != 2 && TurnActions.Instance.currentSlot != 5)
        {
            TurnActions.Instance.discard = false;
        }
    }

    public void RemoveFromGraveEvent()
    {
        RemoveFromCemetery(GameManager.Instance.selectedCard, (int)GameManager.Instance.selectedCard.GetComponent<CardScript2>().myOwner);
    }
    
}
