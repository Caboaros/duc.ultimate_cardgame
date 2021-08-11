using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisual : MonoBehaviour
{
    public static CardVisual Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

    }

    [Header("0 Sun, 1 Moon, 2 Light, 3 Vulcan, 4 Fire, 5 Water, 6 Tree, 7 Wind")]
    // Componentes pra construção de cartas

    //Templates
    public Sprite[] templates;

    //Ícones
    public Sprite[] icons;


    //Cores
    public Color[] textColors;

    // MONK COLORS
    public Material[] monkColors;

    //Strings para icones

    public string sunString = "<font=" + "Icons" + "> 1 ";

    public string moonString = "<font=" + "Icons" + "> 2 ";

    public string mainFontString = "<font=" + "FTLTLT SDF" + ">";

    public string[] iconString = new string[2];

    public string tmpString;

    private void Start()
    {
        //sunString += mainFontString;
        //moonString += mainFontString;
    }

    // Método que define a descrição da carta, pra que seja usada na UI e no resto do jogo
    public void DefineDescription(NeoCard card, string fontAssetName)
    {
        string tmpFontName = "<font=" + fontAssetName + ">";

        iconString[0] = sunString + tmpFontName;
        iconString[1] = moonString + tmpFontName;

        string[] tmpDescription = new string[2];

        if (card.cardType == CardTypes.Magia)
        {
            if (card.cardType == CardTypes.Magia)
            {
                if (card.uniqueStatus != null)
                {
                    tmpDescription[0] = /*card.uniqueStatus.cost + iconString[(int)card.uniqueStatus.cost] +*/ card.uniqueStatus.skill.skillDescription;

                    tmpDescription[0] = tmpDescription[0].Replace("<mana>", card.mana.ToString());
                    tmpDescription[0] = tmpDescription[0].Replace("<cost>", card.uniqueStatus.cost.ToString());
                    tmpDescription[0] = tmpDescription[0].Replace("<value>", card.uniqueStatus.value.ToString());
                }
            }
        }

        else if (card.cardType == CardTypes.Criatura)
        {

        for (int i = 0; i < 2; i++)
        {           

            if (card.status[i] != null)
            {
                tmpDescription[i] = card.status[i].cost + iconString[(int)card.status[i].side] + card.status[i].skill.skillDescription;

                tmpDescription[i] = tmpDescription[i].Replace("<mana>", card.mana.ToString());
                tmpDescription[i] = tmpDescription[i].Replace("<cost>", card.status[i].cost.ToString());
                tmpDescription[i] = tmpDescription[i].Replace("<value>", card.status[i].value.ToString());
            }

            /*if (card.status[i] != null)
            {
                tmpDescription[i] = card.status[i].cost + iconString[(int)card.status[i].side] + card.status[i].skill.skillDescription;

                tmpDescription[i] = tmpDescription[i].Replace("<mana>", card.mana.ToString());
                tmpDescription[i] = tmpDescription[i].Replace("<cost>", card.status[i].cost.ToString());
                tmpDescription[i] = tmpDescription[i].Replace("<value>", card.status[i].value.ToString());
            }*/
        }

        }


        tmpString = tmpDescription[0] + "\n" + tmpDescription[1];

    }

    // Definição de descrição sem símbolos e números
    public void DefineSoleDescription(NeoCard.Status skill)
    {
        tmpString = skill.skill.skillDescription;
        tmpString = tmpString.Replace("<cost>", skill.cost.ToString());
        tmpString = tmpString.Replace("<value>", skill.value.ToString());

    }

}
