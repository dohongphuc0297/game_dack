using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBow : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public LongBow() : base()
    {
        WeaponClassName = "LongBow";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 20;
        Rank = "D";
        Wt = 10;
        Mt = 5;
        Hit = 65;
        Crt = 0;
        Wex = 1;
        int range = 4;
        Range = range;
        Cost = 2000;
    }
}
