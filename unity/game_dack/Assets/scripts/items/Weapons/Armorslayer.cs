using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armorslayer : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Armorslayer() : base()
    {
        WeaponClassName = "Armorslayer";
        Effects = "Effective vs. Armored units";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 30;
        Rank = "D";
        Wt = 10;
        Mt = 8;
        Hit = 75;
        Crt = 0;
        Wex = 1;
        int range = 3;
        Range = range;
        Cost = 600;
    }
}
