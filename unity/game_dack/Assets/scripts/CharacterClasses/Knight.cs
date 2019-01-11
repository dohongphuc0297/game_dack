using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Knight : BaseCharacterClass
{
    public Knight(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Knight";
        CharacterClassDescription = "Soldier has better strength and defence.";
        Level = 1;
        Exp = 0;
        MaxHP = 22;
        HP = 22;
        Strength = 6;
        Magic = 0;
        Skill = 4;
        Speed = 2;
        Luck = 3;
        Defend = 7;
        Resist = 2;
        Movement = 4;
        AttackRange = 1;

        GRHP = 90;
        GRStrength = 45;
        GRMagic = 0;
        GRSkill = 35;
        GRSpeed = 30;
        GRLuck = 30;
        GRDefend = 55;
        GRResist = 20;
    }
}
