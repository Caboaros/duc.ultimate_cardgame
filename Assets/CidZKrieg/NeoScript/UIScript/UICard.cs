using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICard : MonoBehaviour
{
    public GameObject panel;

    public Animator panelAnimator;

    // Sprites

    public Image cardSprite;
    public Image potrait;
    public Image icon;

    // Texto

    public TextMeshProUGUI[] nameTXT;
    public TextMeshProUGUI[] manaTXT;
    public TextMeshProUGUI[] descriptionTXT;
    public TextMeshProUGUI[] typeTXT;

    public void PanelAnimation()
    {
        if (UI.Instance.mouseOverCard)
        {
            if (!panelAnimator.GetBool("Show"))
            {
                panelAnimator.SetBool("Show", true);
            }
        }

        else
        {
            panelAnimator.SetBool("Show", false);
        }
    }

    public void SetInfo(NeoCard card)
    {
        CardVisual.Instance.DefineDescription(card, "MainUI");

        for (int j = 0; j < 2; j++)
        {
            nameTXT[j].text = card.name;
            manaTXT[j].text = card.mana.ToString();
            descriptionTXT[j].text = CardVisual.Instance.tmpString;
            typeTXT[j].text = card.cardType.ToString();
        }

        nameTXT[0].color = CardVisual.Instance.textColors[(int)card.guild];
        manaTXT[0].color = CardVisual.Instance.textColors[(int)card.guild];
        descriptionTXT[0].color = CardVisual.Instance.textColors[(int)card.guild];
        typeTXT[0].color = CardVisual.Instance.textColors[(int)card.guild];

        cardSprite.sprite = CardVisual.Instance.templates[(int)card.guild];
        potrait.sprite = card.potrait;
        icon.sprite = CardVisual.Instance.icons[(int)card.guild];

    }

    public void ShowEvent()
    {
        SetInfo(GameManager.Instance.selectedCard.GetComponent<CardScript2>().cardInfo);

        if (!EventIntervals.Instance.transition)
        {
            PanelAnimation();
        }
    }
}
