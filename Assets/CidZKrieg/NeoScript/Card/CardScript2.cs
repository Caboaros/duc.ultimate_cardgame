using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardScript2 : MonoBehaviour
{
    // Informações da carta
    public NeoCard cardInfo;

    // Atributos

    public NeoCard.Status[] status;

    // Status único pra magia, encantamento e artefato

    public NeoCard.Status effect;

    // Status selecionado (??)

    public NeoCard.Status tmpStatus;

    //////public NeoCard.Value status2;

    public int mana;

    // Textos
    public TextMeshPro nameTXT;
    public TextMeshPro costTXT;
    public TextMeshPro descriptionTXT;
    public TextMeshPro typeTXT;

    // Visual
    public MeshRenderer potraitRenderer;
    public MeshRenderer templateRenderer;
    public MeshRenderer iconRenderer;

    // ====

    public CardTypes myType;
    public Side myOwner;
    public CardState myState;
    //public PlayingState myCondition;
    public int myIndex;

    // Condição

    //public int setTurn;
    //public int awaitingTurns;
    //public bool canActivate;

    public bool canSummon = true;

    // Animador

    public Animator visualsAnim;

    // Renderer de outline

    public Renderer outlineRenderer;

    // Modelo

    public Model cardModel;





    // Mudar o dono da carta
    public void SetCardOwner(Side owner)
    {
        myOwner = owner;
    }

    // Mudar estado da carta
    public void ChangeCardState(CardState state)
    {
        myState = state;

        CheckState();
    }

    public void CheckState()
    {
        if (myState == CardState.Deck)
        {
            transform.SetParent(Board2.Instance.deckHolder[(int)myOwner].transform);
        }

        else if (myState == CardState.Hand)
        {
            transform.SetParent(Board2.Instance.handHolder[(int)myOwner].transform);

            ////////////!!!!!!!!!!!!!!!
            ChangeRotation(transform.parent.localRotation.x + 30,transform.parent.localRotation.y,0);
        }

        else if (myState == CardState.Field)
        {
            transform.SetParent(NeoSlot.Instance.zoneList[myIndex].GetComponent<ZoneScript>().cardPosition.transform);

            /////////////
            ChangeRotation(90, 0, 0);
        }

        else if (myState == CardState.Grave)
        {
            transform.SetParent(Board2.Instance.graveHolder[(int)myOwner].transform);
        }
    }

    public void CanSummon(bool b)
    {
        canSummon = b;
    }

    /*public void CheckCondition()
    {
        if (myCondition == PlayingState.Awaiting)
        {
            canActivate = false;
        }

        if (myCondition == PlayingState.Blocked)
        {
            canActivate = false;
        }

        if (myCondition == PlayingState.Free)
        {
            canActivate = true;
        }
    }*/


    // Métodos de transformação

    public void ResetPosition()
    {

    }

    public void ChangePosition(float x, float y, float z)
    {
        transform.localPosition = new Vector3(x, y, z);
    }

    public void ChangeRotation(float x, float y, float z)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(x,y,z));
    }

    //

    // Animação && Mouse

    private void OnMouseEnter()
    {
        if (GameManager.Instance.GState == GameStates.Match)
        {
            UI.Instance.mouseOverCard = true;

            if (myState == CardState.Hand)
            {
                if (!Battle.Instance.skillChoosing)
                {
                    visualsAnim.SetBool("Show", true);

                    // Audio

                    AudioManager.Instance.SFX.PlayCardSFX("Tap");
                }

                outlineRenderer.enabled = true;

            }

            if (myState == CardState.Field)
            {
                outlineRenderer.enabled = true;
            }
        }

        


    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.GState == GameStates.Match)
        {
            UI.Instance.mouseOverCard = false;

            if (myState == CardState.Hand)
            {
                if (!Battle.Instance.skillChoosing)
                {
                    visualsAnim.SetBool("Show", false);

                    // Audio

                    AudioManager.Instance.SFX.PlayCardSFX("Untap");
                }

            }

            if (outlineRenderer.enabled)
            {
                outlineRenderer.enabled = false;
            }
        }       
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GState == GameStates.Match)
        {
            if (myState != CardState.Deck)
            {
                if (!UI.Instance.mouseOverObject)
                {
                    GameManager.Instance.selectedCard = this.gameObject;
                }
            }

            if (myState == CardState.Hand)
            {
                if (TurnActions.Instance.TPhase == TurnPhase.Action)
                {
                    // Invocação de Resposta
                    /*if (TurnActions.Instance.answer)
                    {

                        for (int i = 0; i < 6; i++)
                        {
                            if (NeoSlot.Instance.zoneList[i].GetComponent<ZoneScript>().myOwner == myOwner)
                            {
                                if (NeoSlot.Instance.zoneList[i].GetComponent<ZoneScript>().state == ZoneState2.Empty)
                                {

                                }

                            }
                        }

                    }*/

                    // Resposta /\

                    if (canSummon)
                    {
                        if (NeoSlot.Instance.zoneList[TurnActions.Instance.currentSlot].GetComponent<ZoneScript>().state == ZoneState2.Empty)
                        {
                            if (NeoSides.Instance.current == myOwner)
                            {
                                if (mana <= Energy2.Instance.energy[(int)myOwner])
                                {

                                    UIButton.Instance.ChangeFunction(ButtonFunction.Summon);
                                }

                                else
                                {
                                    if (UI.Instance.SummonButton.gameObject.activeSelf)
                                    {
                                        UI.Instance.SummonButton.gameObject.SetActive(false);
                                    }
                                }
                            }

                        }

                        if (myType == CardTypes.Magia)
                        {
                            if (mana <= Energy2.Instance.energy[(int)myOwner])
                            {

                            }
                        }
                    }

                }

                else if (TurnActions.Instance.discard)
                {
                    UIButton.Instance.ChangeFunction(ButtonFunction.Discard);
                }

            }

            if (myState == CardState.Field)
            {

                /*if (TurnActions.Instance.TPhase == TurnPhase.Action)
                {

                    if (NeoSides.Instance.current == myOwner)
                    {
                        foreach (NeoCard.Status s in status)
                        {
                            if (s.skill.type == SkillType.Attack)
                            {
                                if (s.cost < Energy2.Instance.energy[(int)myOwner])
                                {
                                    UIButton.Instance.ChangeFunction(ButtonFunction.Attack);

                                    tmpStatus = s;

                                    return;
                                }

                            }
                        }
                    }

                    if (myType == CardTypes.Criatura)
                    {
                        if (TurnActions.Instance.attacking)
                        {
                            UIButton.Instance.ChangeFunction(ButtonFunction.Target);
                        }
                    }

                }

                /*if (status[0].side == NeoSlot.Instance.zoneList[myIndex].GetComponent<ZoneScript>().currentSide)
                {
                    if (status[0].cost < Energy2.Instance.energy[(int)myOwner])
                    {
                        if (status[0].type == ValueType.ATK)
                        {
                            UIButton.Instance.ChangeFunction(ButtonFunction.Attack);
                        }

                        if (status[0].type == ValueType.DEF)
                        {
                            UIButton.Instance.ChangeFunction(ButtonFunction.Defend);
                        }
                    }
                }*/
            }
        }
       
    }
}

// Posição da carta
public enum CardState
{
    Deck,
    Hand,
    Field,
    Grave,
}




