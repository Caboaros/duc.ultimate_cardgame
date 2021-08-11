using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Canvas canvas;

    // UI Display card
    public Text[] nameTXT;
    public Text[] descriptionTXT;
    public Text[] costTXT;
    public Text typeTXT;
    public Image potrait;
    public Image template;
    public bool showingUICard = false;
    public bool mouseOverObject = false;
    public Animator uiCardAnimator;

    // Card Button

    public Button invokeButton;
    public Button graveButton;

    // Yin Yang Text

    public Text yinTXT;
    public Text yangTXT;
    int yinValue;
    int yangValue;

    // Phase info

    public Text phaseTXT;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        yinValue = Energy.Instance.YinMeter;
        yangValue = Energy.Instance.YangMeter;
        yinTXT.text = yinValue.ToString();
        yangTXT.text = yangValue.ToString();

        PhaseText();
    }

    public void DisplayCardUI(Card card, Sprite cardSprite, Color txtColor)
    {
        foreach(Text name in nameTXT)
        {
            name.text = card.cardInfo.name;
        }

        foreach (Text description in descriptionTXT)
        {
            if (card.cardInfo.cardEffect != null)
            {
                description.text = "Efeito: " + card.cardInfo.cardEffect.effectDescription;
            }

            else
            {
                description.text = card.cardInfo.description;
            }
        }

        foreach (Text cost in costTXT)
        {
            cost.text = card.cardInfo.cost.ToString();
        }

        nameTXT[0].color = txtColor;
        descriptionTXT[0].color = txtColor;
        costTXT[0].color = txtColor;

        typeTXT.text = card.cardInfo.type.ToString();

        potrait.sprite = card.cardInfo.potrait;

        template.sprite = cardSprite;

        if (!showingUICard)
        {
        uiCardAnimator.SetBool("Show", true);
        showingUICard = true;
        }
    }

    public void HideUICard()
    {
        if (showingUICard)
        {
        uiCardAnimator.SetBool("Show", false);
        showingUICard = false;
        }
    }

    public void ShowButton(bool show)
    {
        if (graveButton.gameObject.activeSelf)
        {
            graveButton.gameObject.SetActive(false);
        }

        invokeButton.gameObject.SetActive(show);
    }

    public void ShowGraveButton(bool show)
    {
        if (invokeButton.gameObject.activeSelf)
        {
            invokeButton.gameObject.SetActive(false);
        }

        graveButton.gameObject.SetActive(show);
    }

    public void MouseObject(bool b)
    {
        mouseOverObject = b;
    }

    // ======== //
    public void PhaseText()
    {
        phaseTXT.text = Turns.Instance.TPhases.ToString();
    }

    // ======== //

    public void UpdateMeter()
    {
        StartCoroutine(MeterUpdate());
    }

    public IEnumerator MeterUpdate()
    {
        yield return new WaitForSeconds(0.05f);

        // Yin Update
        if (yinValue < Energy.Instance.YinMeter)
        {
            yinValue += 1;
        }

        else if (yinValue > Energy.Instance.YinMeter)
        {
            yinValue -= 1;
        }

        // Yang Update
        if (yangValue < Energy.Instance.YangMeter)
        {
            yangValue += 1;
        }

        else if (yangValue > Energy.Instance.YangMeter)
        {
            yangValue -= 1;
        }

        yinTXT.text = yinValue.ToString();
        yangTXT.text = yangValue.ToString();


        if (yinTXT.text != Energy.Instance.YinMeter.ToString() || yangTXT.text != Energy.Instance.YangMeter.ToString())
        {
            StartCoroutine(MeterUpdate());
        }
    }

}
