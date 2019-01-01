﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelAxe : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public SteelAxe() : base()
    {
        WeaponClassName = "SteelAxe";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 30;
        Rank = "E";
        Wt = 15;
        Mt = 11;
        Hit = 65;
        Crt = 0;
        Wex = 2;
        int range = 2;
        Range = range;
        Cost = 360;
    }
}