using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // Singleton
    public static CardManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public Sprite yinTemplate;
    public Sprite yangTemplate;

    // Cores de texto
    public Color yinColor;
    public Color yangColor;

    public GameObject emptyCard;

    public GameObject selectedCard;

    public void BuildCard(Card card, List<GameObject> deck)
    {
        // Instanciando carta genérica
        GameObject baseCard = Instantiate(emptyCard, transform.position, emptyCard.transform.rotation);

        deck.Add(baseCard);

        // Pegando o script da carta instanciada (esse script guarda valores pra carta)
        CardScript tmpScript = baseCard.GetComponent<CardScript>();
        tmpScript.cardScriptable = card;

        // Template definido conforme tipo da carta
        if (card.cardInfo.type == CardType.Yin)
        {
            tmpScript.frontTemplate.sprite = yinTemplate;
            tmpScript.textColor = yinColor;
            //baseCard.GetComponent<Image>().sprite = yinTemplate;
        }

        else if (card.cardInfo.type == CardType.Yang)
        {
            tmpScript.frontTemplate.sprite = yangTemplate;
            tmpScript.textColor = yangColor;
            //baseCard.GetComponent<Image>().sprite = yangTemplate;
        }

        // Colocando nome na carta instanciada
        tmpScript.name = tmpScript.cardScriptable.cardInfo.name;
        tmpScript.cardName = tmpScript.name;
        
        // Atribuindo nome ao texto name
        tmpScript.nameTXT.text = tmpScript.cardName;
        tmpScript.nameTXT.color = tmpScript.textColor;

        // Atribuindo o custo da carta
        tmpScript.cost = tmpScript.cardScriptable.cardInfo.cost;

        // Atribuindo o valor de custo ao texto cost
        tmpScript.costTXT.text = tmpScript.cost.ToString();
        tmpScript.costTXT.color = tmpScript.textColor;

        // Colocando a descrição na carta
        if (tmpScript.cardScriptable.cardInfo.cardEffect != null)
        {
            tmpScript.descriptionTXT.text = "Efeito: " + tmpScript.cardScriptable.cardInfo.cardEffect.effectDescription;
        }

        else
        {
           tmpScript.descriptionTXT.text = tmpScript.cardScriptable.cardInfo.description;
        }

        tmpScript.descriptionTXT.color = tmpScript.textColor;

        // Colocando cor nos textos

        Sprite tmpSprite = tmpScript.cardScriptable.cardInfo.potrait;
        //tmpSprite.texture.Resize(1000,1000);
        tmpScript.potrait.sprite = tmpSprite;

        tmpScript.CStates = CardStates.OnDeck;

    }

    public void ChangeCardState(GameObject tmpCard, CardStates CStates)
    {
        tmpCard.GetComponent<CardScript>().CStates = CStates;
    }

    public void AddHand(List<GameObject> List, GameObject card)
    {
        List.Add(card);
    }

    public void RemoveHand(List<GameObject> List, GameObject card)
    {
        List.Remove(card);
    }

    public void ToField()
    {
        Zones.Instance.ZoneCheck((int)Sides.Instance.current);
        Zones.Instance.ChangeState((int)Sides.Instance.current ,Zones.Instance.zoneIndex, ZoneState.Filled);

        selectedCard.GetComponent<CardScript>().ChangeState(CardStates.Field);
        selectedCard.GetComponent<CardScript>().ZoneTrigger();
        selectedCard.GetComponent<CardScript>().ChangeIndex(Zones.Instance.zoneIndex);
    }
}
