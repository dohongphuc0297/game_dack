using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : BaseCharacterClass
{
    public Archer(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Archer";
        CharacterClassDescription = "Soldier equipped with bow and attack enemy from far";
        Level = 1;
        Exp = 0;
        MaxHP = 17;
        HP = 17;
        Strength = 4;
        Magic = 0;
        Skill = 5;
        Speed = 6;
        Luck = 4;
        Defend = 3;
        Resist = 2;
        Movement = 5;
        AttackRange = 2;

        GRHP = 55;
        GRStrength = 45;
        GRMagic = 0;
        GRSkill = 45;
        GRSpeed = 60;
        GRLuck = 50;
        GRDefend = 15;
        GRResist = 35;
    }
}
