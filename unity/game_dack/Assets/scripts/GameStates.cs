using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Start,
    PlayerSelectTile,
    PlayerSelectAction,
    PlayerMoveUnit,
    PlayerAttackUnit,
    AnimationFight,
    AnimationPlayerAttack,
    AnimationEnemyAttack,
    AnimationEndFight,
    UnitMoving,
    EnemyTurn,
    GameOver
}
