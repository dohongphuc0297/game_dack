﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Luna : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Luna() : base()
    {
        WeaponClassName = "Luna";
        Effects = "Negates Resistance";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 35;
        Rank = "C";
        Wt = 12;
        Mt = 0;
        Hit = 95;
        Crt = 20;
        Wex = 1;
        int range = 2;
        Range = range;
        Cost = 5250;
    }
}
