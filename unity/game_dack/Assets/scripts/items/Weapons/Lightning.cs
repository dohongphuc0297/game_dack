using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Lightning() : base()
    {
        WeaponClassName = "Lightning";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 36;
        Rank = "E";
        Wt = 6;
        Mt = 4;
        Hit = 95;
        Crt = 5;
        Wex = 1;
        int[] range = {1, 2};
        Range = range;
        Cost = 630;
    }
}
