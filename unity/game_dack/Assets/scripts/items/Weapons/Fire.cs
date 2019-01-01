using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : BaseWeaponClass
{
    public Fire() : base()
    {
        WeaponClassName = "Fire";
        Effects = "";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 40;
        Rank = "E";
        Wt = 4;
        Mt = 5;
        Hit = 90;
        Crt = 0;
        Wex = 1;
        int range = 5;
        Range = range;
        Cost = 560;
    }
}
