using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAxe : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public HandAxe() : base()
    {
        WeaponClassName = "HandAxe";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 20;
        Rank = "E";
        Wt = 12;
        Mt = 7;
        Hit = 60;
        Crt = 0;
        Wex = 1;
        int[] range = {1, 2};
        Range = range;
        Cost = 300;
    }
}
