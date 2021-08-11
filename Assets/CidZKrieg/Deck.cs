using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Singleton
    public static Deck Instance;

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
        // Lista de cartas, deck original do jogador
        // Futuramente, a ideia é importar o Deck dos jogadores através de algum arquivo salvo, e preencher essa lista com base nos decks importados
        public List<Card> rawDeck = new List<Card>();

        // Deck temporário a ser usado na partida, usado pra poder randomizar a lista acima
        public List<Card> tmpDeck = new List<Card>();

        // Lista de cartas construídas
        public List<GameObject> cardList = new List<GameObject>();
        public Stack<GameObject> deckStack = new Stack<GameObject>();

    }

    public Lists[] deckLists;

    //public bool arranged;

    private void Start()
    {

        // Adicionando as cartas da lista "Crua" para a lista temporária
        for (int i = 0; i < 2; i++)
        {
            foreach (Card card in deckLists[i].rawDeck)
            {
                deckLists[i].tmpDeck.Add(card);
            }

            Shuffle(deckLists[i].tmpDeck);

            foreach (Card card in deckLists[i].tmpDeck)
            {
                CardManager.Instance.BuildCard(card, deckLists[i].cardList);
            }

            foreach (GameObject card in deckLists[i].cardList)
            {
                deckLists[i].deckStack.Push(card);
            }

            SetDeckOwner(deckLists[i].cardList, (PlaySides) i);
            ArrangeDeck(deckLists[i].cardList);
            
        }

        StartCoroutine(FirstArrange());

    }

    public void Shuffle(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i ++)
        {
            Card tmpCard = deck[i];

            int v = Random.Range(i, deck.Count);
            deck[i] = deck[v];
            deck[v] = tmpCard;
        }
    }

    public void SetDeckOwner(List<GameObject> list, PlaySides side)
    {
        foreach(GameObject card in list)
        {
            card.GetComponent<CardScript>().SetOwner(side);
        }
    }

    public void ArrangeDeck(List<GameObject> deck)
    {
        float index = 0;

        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject card in deck)
            {
                index++;
                card.transform.position = new Vector3(Board.Instance.deckHolder[i].transform.position.x, Board.Instance.deckHolder[i].transform.position.y + (index / 10), Board.Instance.deckHolder[i].transform.position.z);
            }
        }
        
    }

    public void DrawCard(int amount, int deckID)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tmpCard = deckLists[deckID].deckStack.Pop();
            tmpCard.GetComponent<CardScript>().visualsAnim.SetTrigger("Draw");
            CardManager.Instance.AddHand(Board.Instance.Hands.GetComponent<HandAllign>().handLists[deckID].cardsOnHand, tmpCard);

            Board.Instance.Hands.GetComponent<HandAllign>().AddItem(deckID);
        }
    }

    public IEnumerator FirstArrange()
    {
        float index = 0;

        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject card in deckLists[i].cardList)
            {
                card.transform.SetParent(Board.Instance.deckHolder[i].transform);
                card.transform.eulerAngles = new Vector3(90, Board.Instance.deckHolder[i].transform.eulerAngles.y, card.transform.eulerAngles.z);
                card.SetActive(false);
            }
        }
        

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject card in deckLists[i].cardList)
            {
                yield return new WaitForSeconds(0.1f);

                index++;

                card.SetActive(true);
                card.transform.position = new Vector3(card.transform.parent.transform.position.x, 0 + (index / 50), card.transform.parent.transform.position.z);

            }
        }        

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(BeginningDraw());

    }

    public IEnumerator BeginningDraw()
    {
        yield return new WaitForSeconds(1.5f);

        DrawCard(5, 0);
        DrawCard(5, 1);

        yield return new WaitForSeconds(2f);
        {
            Turns.Instance.ChangePhase(TurnPhases.Draw);
            UIManager.Instance.PhaseText();
        }
    }
}
