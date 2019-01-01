using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : BaseCharacterClass
{
    public Archer(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Archer";
        CharacterClassDescription = "Soldier equipped with bow and attack enemy from far";
        HP = 15;
        Strength = 5;
        Magic = 0;
        Skill = 4;
        Speed = 5;
        Luck = 3;
        Defend = 3;
        Resist = 2;
        Movement = 5;
        AttackRange = 2;
    }
}
