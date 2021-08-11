using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager2 : MonoBehaviour
{
    // Singleton

    public static CardManager2 Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    //Carta genérica vazia (prefab)
    public GameObject blankCard;

    //Texto vazio para criar descrição
    public string tmpString = "0";

    //Método que constrói a carta
    public void BuildCard(NeoCard cardIn, List<GameObject> cardList)
    {
        // Instanciando carta vazia para preencher nelas as informações
        GameObject tmpBlank = Instantiate(blankCard, transform.position, transform.rotation);

        CardScript2 cardScript = tmpBlank.GetComponent<CardScript2>();

        cardScript.cardInfo = cardIn;

        tmpBlank.gameObject.name = cardScript.cardInfo.name;

        cardScript.myType = cardScript.cardInfo.cardType;

        // Atribuindo status

        if (cardScript.myType == CardTypes.Criatura)
        {
            for (int i = 0; i < 2; i++)
            {
                if (cardScript.cardInfo.status[i].skill.type != SkillType.Null)
                {
                    cardScript.status[i].skill = cardScript.cardInfo.status[i].skill;

                    cardScript.status[i].side = cardScript.cardInfo.status[i].side;

                    cardScript.status[i].cost = cardScript.cardInfo.status[i].cost;

                    cardScript.status[i].value = cardScript.cardInfo.status[i].value;
                }

                else
                {
                    cardScript.status[i] = null;
                }
            }
        }

        // Pegando skill da carta caso seja magia
        else if (cardScript.myType == CardTypes.Magia)
        {
            if (cardScript.cardInfo.uniqueStatus != null)
            {
                cardScript.effect = cardScript.cardInfo.uniqueStatus;
            }

        }

        if (cardScript.myType == CardTypes.Magia)
        {
            cardScript.mana = cardScript.effect.cost;
        }

        else if (cardScript.myType == CardTypes.Criatura)
        {
            cardScript.mana = cardScript.cardInfo.mana;
        }

        //cardScript.mana = cardScript.cardInfo.mana;

        // Atribuindo textos
        cardScript.nameTXT.text = cardScript.cardInfo.name;

        cardScript.costTXT.text = cardScript.mana.ToString();

        cardScript.typeTXT.text = cardScript.cardInfo.cardType.ToString();

        // (Descrição)
        CardVisual.Instance.DefineDescription(cardScript.cardInfo, "Main1");
        cardScript.descriptionTXT.text = CardVisual.Instance.tmpString;

        // Atribuindo Visuais

        MaterialTesting.Instance.SetTexture(cardScript.potraitRenderer.material, cardScript.cardInfo.potrait);

        MaterialTesting.Instance.SetTexture(cardScript.templateRenderer.material, CardVisual.Instance.templates[(int)cardScript.cardInfo.guild]);

        MaterialTesting.Instance.SetTexture(cardScript.iconRenderer.material, CardVisual.Instance.icons[(int)cardScript.cardInfo.guild]);

        cardScript.nameTXT.color = CardVisual.Instance.textColors[(int)cardScript.cardInfo.guild];

        cardScript.costTXT.color = CardVisual.Instance.textColors[(int)cardScript.cardInfo.guild];

        cardScript.descriptionTXT.color = CardVisual.Instance.textColors[(int)cardScript.cardInfo.guild];

        cardScript.typeTXT.color = CardVisual.Instance.textColors[(int)cardScript.cardInfo.guild];



        if (cardScript.cardInfo.cardModel != null)
        {
            cardScript.cardModel = cardScript.cardInfo.cardModel;
        }

        // Adicionando à lista de cartas
        cardList.Add(tmpBlank);
    }

    
}
