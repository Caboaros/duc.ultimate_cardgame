using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardScript : MonoBehaviour
{
    public string cardName;
    public TextMeshPro nameTXT;
    public int cost;
    public TextMeshPro costTXT;
    public TextMeshPro descriptionTXT;
    public Color textColor;
    //public Sprite potrait;
    public SpriteRenderer frontTemplate;
    public SpriteRenderer potrait;
    public Animator visualsAnim;
    public CardStates CStates;
    public Card cardScriptable;

    // Índice de carta na mão e zona
    public int myIndex;
    public PlaySides myOwner;

    // Índice de zona, usado quando a carta deve ir para uma zona no campo
    //public int myZone;

    // Método que instancia método do UIManager para mostrar a carta na UI
    public void ShowUI()
    {
        UIManager.Instance.DisplayCardUI(cardScriptable, frontTemplate.sprite, textColor);
    }

    // Método que instancia método do UIManager para mostrar botão de invocação
    public void ShowButton()
    {
        UIManager.Instance.ShowButton(true);
    }

    public void ShowGrave()
    {
        UIManager.Instance.ShowGraveButton(true);
    }

    // Método para dizer se mouse está ou não em cima da carta
    public void MouseOnCard(bool b)
    {
        UIManager.Instance.mouseOverObject = b;
    }

    // Método que mostra menus ao clicar na carta
    public void OnMouseDown()
    {
        // Definindo que a carta clicada é essa
        CardManager.Instance.selectedCard = gameObject;

        // Checando se é possível invocar
        Zones.Instance.CheckState((int)myOwner);

        if (Turns.Instance.TPhases != TurnPhases.Begin)
        {
            if (CStates == CardStates.Hand)
            {
                ShowUI();

                if (Turns.Instance.TPhases == TurnPhases.Main && Zones.Instance.canInvoke)
                {
                    ShowButton();
                }

                
            }

            if (CStates == CardStates.Field)
            {
                ShowUI();

                if (Sides.Instance.current == myOwner)
                {
                    ShowGrave();
                }
            }

        }


    }

    // Método que anima a carta ao posicionar o mouse em cima da mesma e diz que Mouse está sobre uma carta
    private void OnMouseEnter()
    {
        if (Turns.Instance.TPhases != TurnPhases.Begin)
        {
            if (CStates == CardStates.Hand)
            {
                visualsAnim.SetBool("Show", true);
            }

            MouseOnCard(true);
        }
    }

    private void OnMouseExit()
    {
        if (Turns.Instance.TPhases != TurnPhases.Begin)
        {
            if (CStates == CardStates.Hand)
            {
                visualsAnim.SetBool("Show", false);
            }

            MouseOnCard(false);
        }

    }

    public void ChangeState(CardStates state)
    {
        CStates = state;

        ChangeRotation();
    }
    public void ChangeRotation()
    {
        print("Socialismo");

        if (CStates == CardStates.Hand)
        {
            transform.localRotation = Quaternion.Euler(210, 0, 180);
        }

        if (CStates == CardStates.Field)
        {
            transform.localRotation = Quaternion.Euler(-90, 180, 0);
        }
    }

    public void DrawPosition()
    {
        //transform.rotation = Quaternion.Euler(-90, 180, 0);
    }

    public void ZoneTrigger()
    {
        visualsAnim.SetTrigger("Zone");
    }

    // Método chamado em animação para que a carda se estabeleça no campo
    public void ToZone()
    {
        // Container de zonas
        transform.SetParent(Board.Instance.Zones[(int)myOwner].transform);

        // Zona específica
        transform.position = Zones.Instance.zoneList[(int)myOwner].zoneObj[myIndex].zone.transform.position;

        // Definindo qual carta está na zona
        Zones.Instance.zoneList[(int)myOwner].zoneObj[myIndex].cardOnZone = cardScriptable;
    }

    public void ToGrave()
    {
        Zones.Instance.zoneList[(int)myOwner].zoneObj[myIndex].zoneState = ZoneState.Empty;
        Zones.Instance.zoneList[(int)myOwner].zoneObj[myIndex].cardOnZone = null;

        transform.SetParent(Board.Instance.graveHolder[(int)myOwner].transform);
        Graveyard.Instance.AddToGrave(this.gameObject, (int)myOwner);
    }

    public void ChangeIndex(int i)
    {
        myIndex = i;
    }
    public void SetOwner(PlaySides side)
    {
        myOwner = side;
    }
}

public enum CardStates
{
    OnDeck,
    Drawing,
    Hand,
    Field,
}



