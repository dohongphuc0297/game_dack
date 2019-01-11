using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneWalker : BaseCharacterClass
{
    public BoneWalker(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "BoneWalker";
        CharacterClassDescription = "";
        Level = 2;
        Exp = 0;
        MaxHP = 17;
        HP = 17;
        Strength = 5;
        Magic = 0;
        Skill = 1;
        Speed = 5;
        Luck = 0;
        Defend = 2;
        Resist = 2;
        Movement = 5;
        AttackRange = 1;
    }
}
