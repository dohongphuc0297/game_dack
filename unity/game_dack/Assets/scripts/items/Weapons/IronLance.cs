using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronLance : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public IronLance() : base()
    {
        WeaponClassName = "IronLance";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 45;
        Rank = "E";
        Wt = 8;
        Mt = 7;
        Hit = 80;
        Crt = 0;
        Wex = 1;
        int range = 1;
        Range = range;
        Cost = 360;
    }
}
