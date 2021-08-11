using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlaySides mySide;

    public List<GameObject> hand = new List<GameObject>();

    public int handLimit;

    public HandAllign slots;

    GameObject tmpCard;

    private void Start()
    {
        // ?
        //Deck.Instance.StartCoroutine(Deck.Instance.BeginningDraw(this));

        // 
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard(5);
        }*/

        if (Input.GetButtonDown("Fire1") && UIManager.Instance.mouseOverObject == false)
        {
            UIManager.Instance.HideUICard();
            UIManager.Instance.ShowButton(false);
        }

    }

    /*private void LateUpdate()
    {
        if (hand.Count > 0)
        {
            if (tmpCard.GetComponent<CardScript>().CStates == CardStates.Hand)
            {
                ArrangeHand();
            }

            else
            {
                tmpCard = hand[Random.Range(0, hand.Count)];
            }
        }
    }

    public void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            tmpCard = Sides.Instance.decks[(int)mySide].deckStack.Pop();
            tmpCard.GetComponent<CardScript>().visualsAnim.SetTrigger("Draw");
            CardManager.Instance.AddHand(hand, tmpCard);

            slots.AddItem();
        }
    }

    /*public void ArrangeHand()
    {


    }

    public void RemoveFromHand()
    {
        CardManager.Instance.RemoveHand(hand, CardManager.Instance.selectedCard);
        slots.RemoveSlot(CardManager.Instance.selectedCard.GetComponent<CardScript>().myIndex);
    }*/
}
