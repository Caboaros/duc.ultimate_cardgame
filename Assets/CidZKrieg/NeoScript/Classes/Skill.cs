using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardSkill", fileName = "NewSkill")]
public class Skill : ScriptableObject
{
    public SkillType type;

    [TextArea(0, 2)]
    public string skillDescription;

    //public int cost;

    //public int value;

    //public Side side;

}

public enum SkillType
{
    Null,
    Attack,
    Renegerate,
    Steal,
}
