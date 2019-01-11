using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenant : BaseCharacterClass
{
    public Revenant(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Revenant";
        CharacterClassDescription = "";
        Level = 2;
        Exp = 0;
        MaxHP = 19;
        HP = 19;
        Strength = 7;
        Magic = 0;
        Skill = 1;
        Speed = 4;
        Luck = 0;
        Defend = 3;
        Resist = 2;
        Movement = 5;
        AttackRange = 1;
    }
}
