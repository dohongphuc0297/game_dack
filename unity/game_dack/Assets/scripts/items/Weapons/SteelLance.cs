﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelLance : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public SteelLance(GameObject gameObject) : base(gameObject)
    {
        WeaponClassName = "SteelLance";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 30;
        Rank = "D";
        Wt = 13;
        Mt = 10;
        Hit = 70;
        Crt = 0;
        Wex = 2;
        int[] range = {1};
        Range = range;
        Cost = 480;
    }
}