using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public static Players Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    //public Side player1;
    //public Side player2;

    [System.Serializable]
    public class PlayerData
    {
        public string VictoryString;

        public Side mySide;
        public bool hasSummoned;
        public bool hasDiscarded;
        public float playTime;

        public bool ending;
    }

    public PlayerData[] playerData = new PlayerData[2];

    public int winner;

    public bool draw;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            Deck2.Instance.DrawFromDeck((int)NeoSides.Instance.current);
            HandScript.Instance.AddCardToHand(GameManager.Instance.selectedCard, (int)NeoSides.Instance.current);
        }*/
    }

    public void NoSummon()
    {
        for (int i = 0; i < 2; i++)
        {
            playerData[i].hasSummoned = false;
            playerData[i].hasDiscarded = false;
        }
    }

}

