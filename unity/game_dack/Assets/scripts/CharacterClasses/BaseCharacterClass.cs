using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStates
{
    Stance, Active, Left, Right, Up, Down
}

public class BaseCharacterClass
{
    private string characterClassName;
    private string characterClassDescription;

    //stats
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
}
