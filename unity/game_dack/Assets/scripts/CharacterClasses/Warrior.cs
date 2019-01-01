using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BaseCharacterClass
{
    public Warrior(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "BladeMaster";
        CharacterClassDescription = "Master of bow and blade";
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
    }
}
