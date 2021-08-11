using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card", fileName = "New Card")]
public class Card : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public CardType type;
        public string name;
        [TextArea(1,2)]
        public string description;
        public int cost;
        //public Material potrait;
        public Sprite potrait;
        public Effects cardEffect;

    }

    public Info cardInfo;
}

public enum CardType
{ 
    Neutral,
    Yin,
    Yang,
}

