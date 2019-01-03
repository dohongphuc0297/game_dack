using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelBow : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public SteelBow() : base()
    {
        WeaponClassName = "SteelBow";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 30;
        Rank = "D";
        Wt = 9;
        Mt = 9;
        Hit = 70;
        Crt = 0;
        Wex = 1;
        int range = 2;
        Range = range;
        Cost = 720;
    }
}
