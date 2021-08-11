using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIButton : MonoBehaviour
{
    public static UIButton Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

    }

    public TextMeshProUGUI buttonText;

    public UnityEvent SummonEvent;
    public UnityEvent EffectEvent;
    public UnityEvent AttackEvent;
    public UnityEvent DefendEvent;
    public UnityEvent DiscardEvent;

    // Pilha
    public UnityEvent SummonStack;
    public UnityEvent AttackStack;
    public UnityEvent TargetStack;

    // Torre

    public UnityEvent TowerEvent;

    public ButtonFunction BFunction;
    UnityEvent currentEvent;

    //Index temporário para detectar a skill
    public int tmpIndex;

    public void ChangeFunction(ButtonFunction function)
    {
        BFunction = function;
        SetEvent();
    }

    public void SetEvent()
    {
        UI.Instance.SummonButton.gameObject.SetActive(true);

        if (BFunction == ButtonFunction.Summon)
        {
            currentEvent = SummonStack;
            //currentEvent = SummonEvent;
            buttonText.text = "Summon";
        }

        else if (BFunction == ButtonFunction.Effect)
        {
            currentEvent = EffectEvent;
            buttonText.text = "Activate";
        }

        else if (BFunction == ButtonFunction.Attack)
        {
            currentEvent = AttackStack;
            //currentEvent = AttackEvent;
            buttonText.text = "Attack";
        }

        else if (BFunction == ButtonFunction.Target)
        {
            currentEvent = TargetStack;
            buttonText.text = "Target";
        }

        else if (BFunction == ButtonFunction.Defend)
        {
            currentEvent = DefendEvent;
            buttonText.text = "Defense";
        }

        else if (BFunction == ButtonFunction.Discard)
        {
            currentEvent = DiscardEvent;
            buttonText.text = "Discard";
        }


        else if (BFunction == ButtonFunction.Tower)
        {
            currentEvent = TowerEvent;
            buttonText.text = "Activate";
        }
    }


    public void InkoveEvent()
    {
        currentEvent.Invoke();
        //TurnActions.Instance.AnswerEvent.Invoke();
    }
}

public enum ButtonFunction
{
    Summon,
    Effect,
    Attack,
    Defend,
    Discard,
    Target,

    Tower,
}

