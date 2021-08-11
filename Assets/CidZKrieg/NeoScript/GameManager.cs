using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }


    public UnityEvent endGameEvent;

    public UnityEvent startGameEvent;

    public UnityEvent startGameEvent2;

    public UnityEvent startGameEvent3;

    private void Start()
    {
        //ChangeGameStates(GameStates.Match);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeGameStates(GameStates.EndMatch);
        }*/
    }

    public GameObject selectedCard;

    public GameStates GState;

    public void ChangeGameStates(GameStates state)
    {
        GState = state;
        CheckState();
    }

    public void CheckState()
    {
        if (GState == GameStates.EndMatch)
        {
            endGameEvent.Invoke();
        }

        else if (GState == GameStates.StartMatch)
        {
            startGameEvent.Invoke();
        }
    }

    public void ChangeToMatch()
    {
        ChangeGameStates(GameStates.Match);
    }
}

public enum GameStates
{
    Match,
    StartMatch,
    EndMatch,
}

