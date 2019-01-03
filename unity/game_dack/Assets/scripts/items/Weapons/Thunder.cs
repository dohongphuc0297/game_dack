using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Thunder() : base()
    {
        WeaponClassName = "Thunder";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 35;
        Rank = "D";
        Wt = 6;
        Mt = 8;
        Hit = 80;
        Crt = 5;
        Wex = 1;
        int range = 2;
        Range = range;
        Cost = 700;
    }
}
