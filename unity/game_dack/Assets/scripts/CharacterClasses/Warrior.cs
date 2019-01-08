using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BaseCharacterClass
{
    public Warrior(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "BladeMaster";
        CharacterClassDescription = "Master of bow and blade";
        MaxHP = 20;
        HP = 20;
        Strength = 7;
        Magic = 0;
        Skill = 10;
        Speed = 10;
        Luck = 5;
        Defend = 4;
        Resist = 2;
        Movement = 4;
        AttackRange = 1;

        GRHP = 60;
        GRStrength = 60;
        GRMagic = 0;
        GRSkill = 60;
        GRSpeed = 60;
        GRLuck = 50;
        GRDefend = 25;
        GRResist = 35;
    }
}
