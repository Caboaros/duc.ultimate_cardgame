using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    [Header("Script que ativa e desativa objetos da HUD dependendo do estado da partida")]

    public List<GameObject> transitionInvisible;

    public List<GameObject> battleInvisible;

    public List<GameObject> answerInvisible;

    public List<GameObject> automaticPhaseInvisible;

    public List<GameObject> cameraRotationVisible;


    public List<GameObject> disableList;

    public List<GameObject> enableList;

    public List<GameObject> endGameList;

    public void CheckVisibility()
    {
        if (EventIntervals.Instance.transition)
        {
            foreach (GameObject item in transitionInvisible)
            {
                item.SetActive(false);
            }
        }

        else if (!EventIntervals.Instance.transition)
        {
            foreach (GameObject item in transitionInvisible)
            {
                item.SetActive(true);
            }
        }


    }

    public void CheckBattleVisibility()
    {
        if (TurnActions.Instance.attacking)
        {
            foreach (GameObject item in battleInvisible)
            {
                item.SetActive(false);
            }
        }

        else if (!TurnActions.Instance.attacking)
        {
            foreach (GameObject item in battleInvisible)
            {
                item.SetActive(true);
            }
        }
    }

    public void CheckAnswerVisibility()
    {
        if (TurnActions.Instance.answer)
        {
            foreach (GameObject item in answerInvisible)
            {
                item.SetActive(false);
            }
        }

        else if (!TurnActions.Instance.attacking)
        {
            foreach (GameObject item in answerInvisible)
            {
                item.SetActive(true);
            }
        }
    }

    public void CheckAutomaticPhase()
    {
        if (TurnActions.Instance.automaticPhase)
        {
            print("SaiNext1");
            foreach (GameObject item in automaticPhaseInvisible)
            {
                item.gameObject.SetActive(false);
            }
        }

        else if (!TurnActions.Instance.automaticPhase)
        {
            foreach (GameObject item in automaticPhaseInvisible)
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    public void CheckEndGameVisibility()
    {
        if (GameManager.Instance.GState == GameStates.EndMatch)
        {
            foreach(GameObject item in endGameList)
            {
                item.SetActive(false);
            }
        }
    }

    public void DisableObjects()
    {
        foreach(GameObject item in disableList)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void EnableObjects()
    {
        foreach(GameObject item in enableList)
        {
            item.gameObject.SetActive(true);
        }
    }

}
