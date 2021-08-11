using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    public Animator anim;

    public TextMeshProUGUI screenText;

    public Image icon;

    public Sprite[] iconSprite;
    //public Sprite moonIcon;

    public UnityEvent transitionEvent;

    public void Start()
    {
        anim.GetComponent<Animator>();
    }

    public void Transition()
    {
        screenText.text = TurnActions.Instance.TPhase.ToString();
        anim.SetTrigger("Transition");
    }

    public void EndGameTransition()
    {

        anim.SetTrigger("End");



    }

    public void EndGameInfo()
    {

        if (Players.Instance.draw)
        {
            screenText.text = "Empate.";
            icon.gameObject.SetActive(false);
        }

        else
        {
            print("Alguém ganhou");
            screenText.text = Players.Instance.playerData[Players.Instance.winner].VictoryString;
            icon.sprite = iconSprite[(int)Players.Instance.playerData[Players.Instance.winner].mySide];
        }
    }

    // Usado em animação.
    public void AnimEvent()
    {
        if (GameManager.Instance.GState != GameStates.EndMatch)
        {
            transitionEvent.Invoke();
        }

    }

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }

    public void StartGame()
    {
        anim.SetTrigger("StartGame");

        screenText.text = "START";
    }
}
