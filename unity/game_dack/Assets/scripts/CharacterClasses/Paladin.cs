using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : BaseCharacterClass
{
    public Paladin(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Paladin";
        CharacterClassDescription = "Soldier has better strength and speed.";
        Level = 1;
        Exp = 0;
        MaxHP = 20;
        HP = 20;
        Strength = 7;
        Magic = 0;
        Skill = 5;
        Speed = 7;
        Luck = 2;
        Defend = 6;
        Resist = 1;
        Movement = 7;
        AttackRange = 1;

        GRHP = 80;
        GRStrength = 40;
        GRMagic = 0;
        GRSkill = 40;
        GRSpeed = 50;
        GRLuck = 40;
        GRDefend = 25;
        GRResist = 20;
    }
}
