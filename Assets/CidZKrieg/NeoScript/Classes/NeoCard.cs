using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NeoCard : ScriptableObject
{
    public CardTypes cardType;
    public GuildType guild;

    // Classe para os atributos das cartas
    public string cardName;
    //[TextArea(1,3)]

    public Sprite potrait;

    public Model cardModel;

    //[System.Serializable]
    /*public class Value
    {
        public float value;
        public Side side;
        public ValueType type;
        public int cost;
    }*/

    [System.Serializable]
    public class Status
    {
        public Skill skill;
        public Side side;
        public int cost;
        public int value;
    }

    public Status[] status = new Status[2];

    public int mana;

    // Status pra magia, encantamento, etc
    public Status uniqueStatus;
}

public enum CardTypes
{
    Criatura,
    Magia,
    Encantamento,
    Artefato,
}

public enum GuildType
{
    sun = 0,
    moon = 1,
    light = 2,
    vulcan = 3,
    fire = 4,
    water = 5,
    tree = 6,
    wind = 7,
}
