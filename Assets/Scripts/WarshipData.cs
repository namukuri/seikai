using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class WarshipData
{
    public int warshipNo;
    public string warshipName;
    public Sprite warshipSprite;
    public WarshipTypes warshipTypes;
    public int hp;
    public int attackPower;
    public AttackRangeTypes attackRangeTypes;
    public int defensePower;
    public int mobility;
}
