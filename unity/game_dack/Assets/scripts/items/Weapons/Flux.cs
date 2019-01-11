using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Flux : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Flux() : base()
    {
        WeaponClassName = "Flux";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 45;
        Rank = "D";
        Wt = 8;
        Mt = 7;
        Hit = 80;
        Crt = 0;
        Wex = 1;
        int range = 2;
        Range = range;
        Cost = 900;
    }
}
