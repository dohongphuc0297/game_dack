using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStates
{
    Stance, Active, Left, Right, Up, Down
}

[System.Serializable]
public class BaseCharacterClass
{
    private string characterClassName;
    private string characterClassDescription;

    //stats
    private int level;
    private float exp;
    private int maxhitpoint;
    private int hitpoint;
    private int strength;
    private int magic;
    private int skill;
    private int speed;
    private int luck;
    private int defend;
    private int resist;
    private int movement;

    private int grhitpoint;
    private int grstrength;
    private int grmagic;
    private int grskill;
    private int grspeed;
    private int grluck;
    private int grdefend;
    private int grresist;

    public int AttackRange { get; set; }

    public BaseWeaponClass EquippedWeapon;

    //state
    private CharacterStates state;

    public BaseCharacterClass(GameObject gameObject)
    {
        _GameObject = gameObject;
        _Animator = _GameObject.GetComponent<Animator>();
        State = CharacterStates.Stance;
        EquippedWeapon = new BaseWeaponClass();
    }

    public GameObject _GameObject { get; set; }
    public Animator _Animator { get; set; }

    public CharacterStates State
    {
        get { return state; }
        set { state = value; }
    }
    public string CharacterClassName
    {
        get { return characterClassName; }
        set { characterClassName = value; }
    }
    public string CharacterClassDescription
    {
        get { return characterClassDescription; }
        set { characterClassDescription = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public float Exp
    {
        get { return exp; }
        set { exp = value; }
    }

    public int MaxHP
    {
        get { return maxhitpoint; }
        set { maxhitpoint = value; }
    }
 
    public int HP
    {
        get { return hitpoint; }
        set { hitpoint = value; }
    }
    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    public int Magic
    {
        get { return magic; }
        set { magic = value; }
    }
    public int Skill
    {
        get { return skill; }
        set { skill = value; }
    }
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public int Luck
    {
        get { return luck; }
        set { luck = value; }
    }
    public int Defend
    {
        get { return defend; }
        set { defend = value; }
    }
    public int Resist
    {
        get { return resist; }
        set { resist = value; }
    }
    public int Movement
    {
        get { return movement; }
        set { movement = value; }
    }

    public int GRHP
    {
        get { return grhitpoint; }
        set { grhitpoint = value; }
    }
    public int GRStrength
    {
        get { return grstrength; }
        set { grstrength = value; }
    }
    public int GRMagic
    {
        get { return grmagic; }
        set { grmagic = value; }
    }
    public int GRSkill
    {
        get { return grskill; }
        set { grskill = value; }
    }
    public int GRSpeed
    {
        get { return grspeed; }
        set { grspeed = value; }
    }
    public int GRLuck
    {
        get { return grluck; }
        set { grluck = value; }
    }
    public int GRDefend
    {
        get { return grdefend; }
        set { grdefend = value; }
    }
    public int GRResist
    {
        get { return grresist; }
        set { grresist = value; }
    }

    public void setInfo(BaseCharacterClass Char)
    {
        Level = Char.Level;
        Exp = Char.Exp;
        MaxHP = Char.MaxHP;
        HP = Char.HP;
        Strength = Char.Strength;
        Magic = Char.Magic;
        Skill = Char.Skill;
        Speed = Char.Speed;
        Luck = Char.Luck;
        Defend = Char.Defend;
        Resist = Char.Resist;
        Movement = Char.Movement;
        AttackRange = Char.AttackRange;

        GRHP = Char.GRHP;
        GRStrength = Char.GRStrength;
        GRMagic = Char.GRMagic;
        GRSkill = Char.GRSkill;
        GRSpeed = Char.GRSpeed;
        GRLuck = Char.GRLuck;
        GRDefend = Char.GRDefend;
        GRResist = Char.GRResist;
    }
}
