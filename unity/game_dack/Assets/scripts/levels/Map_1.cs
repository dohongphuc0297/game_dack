using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_1 : MonoBehaviour
{
    private GameStates currentState;
    private BaseCharacterClass Lyn = new Warrior();
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameStates.Start;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameStates.Start:
                break;
            case GameStates.PlayerSelectTile:
                break;
            case GameStates.PlayerSelectAction:
                break;
            case GameStates.PlayerMoveUnit:
                break;
            case GameStates.PlayerAttackUnit:
                break;
            case GameStates.EnemyTurn:
                break;
            case GameStates.GameOver:
                break;
            default:
                break;
        }
    }

    void OnGUI()
    {
    }
}
