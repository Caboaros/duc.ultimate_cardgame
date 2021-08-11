using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SleepType
{
    Null,
    Activation,
    StealBuff,
    
}

public class WaitingList : MonoBehaviour
{
    public static WaitingList Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    // Old
    public List<GameObject> awaitingCardList = new List<GameObject>();

    [System.Serializable]
    public class SleepingObject
    {
        public GameObject tower;
        public SleepType sleepType;
        public int setTurn;
        public int turnsToAwake;

        // valor para buffs
        public int value;
    }

    public List<SleepingObject> sleepingList = new List<SleepingObject>();


    public List<GameObject> awaitingTowerList = new List<GameObject>();
 
    public SleepingObject tmpSleep;

    // Old
    /*public void AddToWaitingList(GameObject card)
    {
        awaitingCardList.Add(card);

    }*/

    // Old
    /*public void CheckAwaitingCards()
    {
        if (awaitingCardList.Count > 0)
        {
            for (int i = 0; i < awaitingCardList.Count; i++)
            {
                GameObject card = awaitingCardList[i];

                if (TurnActions.Instance.turnCount >= (card.GetComponent<CardScript2>().setTurn + card.GetComponent<CardScript2>().awaitingTurns))
                {
                    card.GetComponent<CardScript2>().ChangeCardCondition(PlayingState.Free);
                    awaitingCardList.Remove(card);
                }

            }
        }
    }*/

    /*public void AddToWaitingList(GameObject tower, SleepType type)
    {
        public SleepingObject tmpSleep = new SleepingObject();

    }*/

    public void AddToActivationSleepList(GameObject tower, SleepType type, int turnsToReturn)
    {
        ClearTMP();

        tmpSleep.tower = tower;
        tmpSleep.sleepType = type;
        tmpSleep.setTurn = TurnActions.Instance.turnCount;
        tmpSleep.turnsToAwake = turnsToReturn;

        SleepingObject tmp2 = tmpSleep;

        sleepingList.Add(tmp2);

    }

    public void AddToStealSleepList(GameObject tower, SleepType type, int turnsToReturn, int value)
    {
        ClearTMP();

        tmpSleep.tower = tower;
        tmpSleep.sleepType = type;
        tmpSleep.setTurn = TurnActions.Instance.turnCount;
        tmpSleep.turnsToAwake = turnsToReturn;

        tmpSleep.value = value;

        SleepingObject tmp2 = tmpSleep;

        sleepingList.Add(tmp2);
    }

    public void CheckSleepingList()
    {
        if (sleepingList.Count > 0)
        {
            for (int i = 0; i < sleepingList.Count; i++)
            {
                if (sleepingList[i].sleepType == SleepType.Activation)
                {

                    if (TurnActions.Instance.turnCount >= sleepingList[i].setTurn + sleepingList[i].turnsToAwake)
                    {
                        sleepingList[i].tower.GetComponent<ZoneScript>().sleeping = false;
                        sleepingList.Remove(sleepingList[i]);
                    }

                }

                else if (sleepingList[i].sleepType == SleepType.StealBuff)
                {
                    if (TurnActions.Instance.turnCount >= sleepingList[i].setTurn + sleepingList[i].turnsToAwake)
                    {

                    }
                }

            }
        }
    }

    public void ClearTMP()
    {
        tmpSleep.tower = null;
        tmpSleep.turnsToAwake = 0;
        tmpSleep.sleepType = SleepType.Null;
        tmpSleep.setTurn = 0;
    }

}
