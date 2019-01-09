using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brigand : BaseCharacterClass
{
    public Brigand(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Brigand";
        CharacterClassDescription = "Bandit who use axe and destroy building when visit";
        Level = 1;
        Exp = 0;
        MaxHP = 20;
        HP = 20;
        Strength = 6;
        Magic = 0;
        Skill = 2;
        Speed = 3;
        Luck = 1;
        Defend = 2;
        Resist = 1;
        Movement = 5;
        AttackRange = 1;
    }
}
