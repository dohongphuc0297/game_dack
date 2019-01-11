using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IronSword : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public IronSword() : base()
    {
        WeaponClassName = "IronSword";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 46;
        Rank = "E";
        Wt = 5;
        Mt = 5;
        Hit = 90;
        Crt = 0;
        Wex = 1;
        int range = 1;
        Range = range;
        Cost = 460;
    }
}
