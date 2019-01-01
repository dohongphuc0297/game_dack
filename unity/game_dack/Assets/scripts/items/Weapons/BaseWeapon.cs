using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponClass
{
    private string weaponClassName;
    private string effects;

    //private string itemClassDescription;

    //stats
    private int uses;
    private string rank;
    private int wt; //Weight 
    private int mt; //Weapon's attack power.
    private int hit; //Weapon's Hit rate + [(Skill x 3 + Luck) / 2] + Weapon Rank bonus
    private int crt;
    private int wex; //Weapon EXP
    private int range;
    private int cost;

    //state
    //private CharacterStates state;

    public BaseWeaponClass()
    {
        //_GameObject = gameObject;
        int r = 0 ;
        range = r;
        //_Animator = _GameObject.GetComponent<Animator>();
        //State = CharacterStates.Stance;
    }

    public GameObject _GameObject { get; set; }
    //public Animator _Animator { get; set; }


    //public CharacterStates State
    //{
    //    get { return state; }
    //    set { state = value; }
    //}

    public string WeaponClassName
    {
        get { return weaponClassName; }
        set { weaponClassName = value; }
    }
    public string Effects
    {
        get { return effects; }
        set { effects = value; }
    }
    //public string CharacterClassDescription
    //{
    //    get { return characterClassDescription; }
    //    set { characterClassDescription = value; }
    //}

    public int Uses
    {
        get { return uses; }
        set { uses = value; }
    }
    public string Rank
    {
        get { return rank; }
        set { rank = value; }
    }
    public int Wt
    {
        get { return wt; }
        set { wt = value; }
    }
    public int Mt
    {
        get { return mt; }
        set { mt = value; }
    }
    public int Hit
    {
        get { return hit; }
        set { hit = value; }
    }
    public int Crt
    {
        get { return crt; }
        set { crt = value; }
    }
    public int Wex
    {
        get { return wex; }
        set { wex = value; }
    }
    public int Range
    {
        get { return range; }
        set { range = value; }
    }
    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }
}
