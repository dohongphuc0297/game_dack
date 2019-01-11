using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IronBow : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public IronBow() : base()
    {
        WeaponClassName = "IronBow";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 45;
        Rank = "E";
        Wt = 5;
        Mt = 6;
        Hit = 85;
        Crt = 0;
        Wex = 1;
        int range = 2;
        Range = range;
        Cost = 540;
    }
}
