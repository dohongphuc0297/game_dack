using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mend : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Mend(GameObject gameObject) : base(gameObject)
    {
        WeaponClassName = "Mend";
        Effects = "Restores HP to an adjacent ally equal to (Magic +20). Provides 12 EXP to the user";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 20;
        Rank = "D";
        Wt = 0;
        Mt = 0;
        Hit = 0;
        Crt = 0;
        Wex = 3;
        int[] range = {1};
        Range = range;
        Cost = 1000;
    }
}
