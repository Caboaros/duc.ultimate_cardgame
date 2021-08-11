using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect", fileName = "New Effect")]
[System.Serializable]
public class Effects : ScriptableObject
{
    public EffectType effect;
    public int amount;
    [TextArea(1,2)]
    public string effectDescription;

}

public enum EffectType
{
    AttributeRaise,
    NoCost,
}

