using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Heal(GameObject gameObject) : base(gameObject)
    {
        WeaponClassName = "Heal";
        Effects = "Restores HP to an adjacent ally equal to (Magic +10). Provides 11 EXP to the user";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 30;
        Rank = "E";
        Wt = 0;
        Mt = 0;
        Hit = 0;
        Crt = 0;
        Wex = 2;
        int[] range = {1};
        Range = range;
        Cost = 600;
    }
}
