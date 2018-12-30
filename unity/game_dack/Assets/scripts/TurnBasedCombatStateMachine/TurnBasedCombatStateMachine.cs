using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedCombatStateMachine : MonoBehaviour
{
    public enum BattleStates
    {
        START,
        PLAYERCHOICE,
        ENEMYCHOICE,
        LOSE,
        WIN
    }

    private BattleStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BattleStates.START;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case BattleStates.START:
                break;
            case BattleStates.PLAYERCHOICE:
                break;
            case BattleStates.ENEMYCHOICE:
                break;
            case BattleStates.LOSE:
                break;
            case BattleStates.WIN:
                break;
            default:
                break;
        }
    }

    void onGUI()
    {
        if (GUILayout.Button("NEXT STATE"))
        {
            if(currentState == BattleStates.START)
            {
                currentState = BattleStates.PLAYERCHOICE;
            }
        }
    }
}
