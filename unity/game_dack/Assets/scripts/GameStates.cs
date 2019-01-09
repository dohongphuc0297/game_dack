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
    AnimationPlayerDeath,
    AnimationEnemyDeath,
    AnimationEndFight,
    UnitMoving,
    ToEnemyTurn,
    EnemyTurn,
    EnemyUnitMoving,
    EnemyUnitAttack,
    AfterAnimationFight,
    GameOver
}
