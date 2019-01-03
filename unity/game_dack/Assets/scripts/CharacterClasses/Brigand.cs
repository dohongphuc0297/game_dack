using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brigand : BaseCharacterClass
{
    public Brigand(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Brigand";
        CharacterClassDescription = "Bandit who use axe and destroy building when visit";
        MaxHP = 20;
        HP = 20;
        Strength = 7;
        Magic = 0;
        Skill = 3;
        Speed = 2;
        Luck = 1;
        Defend = 2;
        Resist = 1;
        Movement = 3;
        AttackRange = 1;
    }
}
