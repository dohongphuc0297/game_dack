using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronAxe : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public IronAxe() : base()
    {
        WeaponClassName = "IronAxe";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 45;
        Rank = "E";
        Wt = 10;
        Mt = 8;
        Hit = 75;
        Crt = 0;
        Wex = 1;
        int[] range = {1};
        Range = range;
        Cost = 270;
    }
}
