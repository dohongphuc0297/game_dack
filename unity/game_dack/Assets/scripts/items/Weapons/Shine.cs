﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Shine(GameObject gameObject) : base(gameObject)
    {
        WeaponClassName = "Shine";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 30;
        Rank = "D";
        Wt = 8;
        Mt = 6;
        Hit = 90;
        Crt = 8;
        Wex = 2;
        int[] range = {1, 2};
        Range = range;
        Cost = 900;
    }
}