using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nosferatu : BaseWeaponClass
{
    public int AttackRange { get; set; }
    public Nosferatu(GameObject gameObject) : base(gameObject)
    {
        WeaponClassName = "Nosferatu";
        Effects = "Restores HP equal to damage dealt";
        //ItemsClassDescription = "Soldier equipped with bow and attack enemy from far";
        Uses = 20;
        Rank = "C";
        Wt = 14;
        Mt = 10;
        Hit = 70;
        Crt = 0;
        Wex = 1;
        int[] range = {1, 2};
        Range = range;
        Cost = 3200;
    }
}
