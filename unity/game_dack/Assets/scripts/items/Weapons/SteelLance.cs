using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelLance : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public SteelLance() : base()
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
        int range = 3;
        Range = range;
        Cost = 480;
    }
}
