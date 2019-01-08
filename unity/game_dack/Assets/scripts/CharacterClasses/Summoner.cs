using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : BaseCharacterClass
{
    public Summoner(GameObject gameObject) : base(gameObject)
    {
        CharacterClassName = "Summoner";
        CharacterClassDescription = "Main character";
        Level = 1;
        Exp = 0;
        MaxHP = 18;
        HP = 18;
        Strength = 0;
        Magic = 7;
        Skill = 6;
        Speed = 8;
        Luck = 2;
        Defend = 2;
        Resist = 6;
        Movement = 5;
        AttackRange = 2;

        GRHP = 50;
        GRStrength = 0;
        GRMagic = 60;
        GRSkill = 60;
        GRSpeed = 40;
        GRLuck = 30;
        GRDefend = 20;
        GRResist = 60;
    }
}