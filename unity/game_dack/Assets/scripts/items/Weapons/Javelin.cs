using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Javelin() : base()
    {
        WeaponClassName = "Javelin";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 20;
        Rank = "E";
        Wt = 11;
        Mt = 6;
        Hit = 65;
        Crt = 0;
        Wex = 1;
        int range = 2;
        Range = range;
        Cost = 400;
    }
}
