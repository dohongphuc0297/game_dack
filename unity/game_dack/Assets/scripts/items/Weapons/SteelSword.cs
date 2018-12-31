using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelSword : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public SteelSword(GameObject gameObject) : base(gameObject)
    {
        WeaponClassName = "SteelSword";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 30;
        Rank = "D";
        Wt = 10;
        Mt = 8;
        Hit = 75;
        Crt = 0;
        Wex = 1;
        int[] range = {1};
        Range = range;
        Cost = 600;
    }
}
