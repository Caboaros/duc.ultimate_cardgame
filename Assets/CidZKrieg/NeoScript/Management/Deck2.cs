using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck2 : MonoBehaviour
{
    // Singleton

    public static Deck2 Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    // RawDeck

    [System.Serializable]
    public class Decks
    {
        // Lista que recebe o "deck original" (A intenção é que a lista seja preenchida carregando algum arquivo salvo, como json
        public List<NeoCard> rawDeck = new List<NeoCard>();

        // Lista que recebe as cartas da lista acima, para fazer embaralhamento e depois construir as cartas "físicas"
        public List<NeoCard> tmpDeck = new List<NeoCard>();

        // Lista das cartas construídas
        public List<GameObject> cardDeck = new List<GameObject>();

        public Stack<GameObject> cardStack = new Stack<GameObject>();
    }

    public Decks[] decks;

    public int initialDrawAmount;

    public void AddToTmpDeck()
    {
        for (int i = 0; i < 2; i++)
        {
            foreach(NeoCard card in decks[i].rawDeck)
            {
                decks[i].tmpDeck.Add(card);
            }

            Shuffle(decks[i].tmpDeck);
        }

    }

    public void Shuffle(List<NeoCard> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            NeoCard tmpCard = deck[i];

            int v = Random.Range(i, deck.Count);
            deck[i] = deck[v];
            deck[v] = tmpCard;
        }
    }

    public void BuildDeck(List<NeoCard> deck, List<GameObject> cardList)
    {
        foreach (NeoCard card in deck)
        {
            CardManager2.Instance.BuildCard(card, cardList);
        }
    }

    public void ArrangeStack(List<GameObject> deck, Stack<GameObject> stack)
    {
        foreach (GameObject card in deck)
        {
            stack.Push(card);
        }
    }

    // Método que retira a primeira carta da pilha indicada e a destaca como carta selecionada
    public void DrawFromDeck(int ID)
    {
        GameManager.Instance.selectedCard = decks[ID].cardStack.Pop();
        decks[ID].cardDeck.Remove(GameManager.Instance.selectedCard);
        GameManager.Instance.selectedCard.GetComponent<CardScript2>().ChangeCardState(CardState.Hand);
    }

    public void RemoveFromDeck(GameObject card)
    {
        int c = (int)card.GetComponent<CardScript2>().myOwner;

        if (decks[c].cardDeck.Contains(card))
        {
            decks[c].cardDeck.Remove(card);
        }

        decks[c].cardStack.Clear();

        ArrangeStack(decks[c].cardDeck, decks[c].cardStack);

    }

    public void FirstDraw()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < initialDrawAmount; j++)
            {
                DrawFromDeck(i);
                HandScript.Instance.AddCardToHand(GameManager.Instance.selectedCard, i);
            }
        }
    }

    public void ArrangeDeck(List<GameObject> cardList)
    {
        float ind = 0;

        foreach (GameObject card in cardList)
        {
            ind++;
            CardScript2 cardScript = card.GetComponent<CardScript2>();

            cardScript.ChangeCardState(CardState.Deck);
            cardScript.ChangePosition(0, ind / 100, 0);
            cardScript.ChangeRotation(-90, Board2.Instance.deckHolder[(int)cardScript.myOwner].transform.rotation.y, 0);
        }
    }

    public void FirstEvent()
    {
        // Construindo deck
        AddToTmpDeck();

        for (int i = 0; i < 2; i++)
        {
            BuildDeck(decks[i].tmpDeck, decks[i].cardDeck);

            foreach (GameObject card in decks[i].cardDeck)
            {
                card.GetComponent<CardScript2>().SetCardOwner((Side)i);
            }

            ArrangeDeck(decks[i].cardDeck);
            ArrangeStack(decks[i].cardDeck, decks[i].cardStack);
        }

        // Compra inicial
        //FirstDraw();

        //InvokeFirstAnimation();
    }

    public void InvokeFirstAnimation()
    {
        StartCoroutine(FirstAnimation());
        //StartCoroutine(FirstAnimation(1));
    }

    public IEnumerator FirstAnimation()
    {
        foreach (GameObject card in decks[0].cardDeck)
        {
            card.SetActive(false);
        }

        foreach (GameObject card in decks[1].cardDeck)
        {
            card.SetActive(false);
        }

        yield return new WaitForSeconds(2f);

        AudioManager.Instance.SFX.PlayCardSFX("Shuffle");

        for (int i = 0; i < decks[0].cardDeck.Count; i++)
        {
            yield return new WaitForSeconds(0.03f);
            decks[0].cardDeck[i].SetActive(true);
            decks[0].cardDeck[i].GetComponent<CardScript2>().visualsAnim.SetTrigger("Slide");

            
        }

        AudioManager.Instance.SFX.PlayCardSFX("Shuffle");

        for (int i = 1; i < decks[1].cardDeck.Count; i++)
        {
            yield return new WaitForSeconds(0.03f);
            decks[1].cardDeck[i].SetActive(true);
            decks[1].cardDeck[i].GetComponent<CardScript2>().visualsAnim.SetTrigger("Slide");
        }

        yield return new WaitForSeconds(1f);
        FirstDraw();
        AudioManager.Instance.SFX.PlayCardSFX("Draw");

        yield return new WaitForSeconds(2f);
        GameManager.Instance.startGameEvent2.Invoke();

        yield return new WaitForSeconds(2.1f);
        GameManager.Instance.startGameEvent3.Invoke();
    }
}
