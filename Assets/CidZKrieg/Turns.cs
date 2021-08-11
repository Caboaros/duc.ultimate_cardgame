using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TurnPhases
{
    Begin = 0,
    Attribute = 1,
    Draw = 2,
    Main = 3,
    Transition = 4,

    /*Inicio = 0,
    Manutenção = 1,
    Compra = 2,
    Principal = 3,
    Resolução = 4,*/

}
public class Turns : MonoBehaviour
{
    public static Turns Instance;

    public int turnCount;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public TurnPhases TPhases;

    public UnityEvent AttributeEvent;
    public UnityEvent DrawEvent;
    public UnityEvent TransitionEvent;

    public void ChangePhase(TurnPhases phase)
    {
        TPhases = phase;
        PhaseEvent();
    }

    public void NextPhase()
    {
        if (TPhases < (TurnPhases)4)
        {
            TPhases++;
        }

        else
        {
            TPhases = (TurnPhases)1;
        }

        PhaseEvent();
    }

    public void TurnCount()
    {
        turnCount++;
    }

    // Compra de turno
    public void PhaseDraw()
    {
        Deck.Instance.DrawCard(1, (int)Sides.Instance.current);
    }


    public void PhaseEvent()
    {
        if (TPhases == TurnPhases.Attribute)
        {
            AttributeEvent.Invoke();
        }

        else if (TPhases == TurnPhases.Draw)
        {
            DrawEvent.Invoke();
        }

        else if (TPhases == TurnPhases.Main)
        {

        }

        else if (TPhases == TurnPhases.Transition)
        {
            TransitionEvent.Invoke();
        }

    }

}
