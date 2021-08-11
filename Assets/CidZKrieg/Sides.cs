using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sides : MonoBehaviour
{
    public static Sides Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public PlaySides current;

    public Deck decks;

    public void ChangeSide()
    {
        if (current == PlaySides.Player1)
        {
            current = PlaySides.Player2;
        }

        else
        {
            current = PlaySides.Player1;
        }
    }
}

public enum PlaySides
{
    Player1 = 0,
    Player2 = 1,
}

