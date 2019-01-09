using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void KillConfirmed(BaseCharacterClass character);

public class GameManager : MonoBehaviour
{
    public event KillConfirmed killComfirmEvent;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public void OnKillConfirmed(BaseCharacterClass character)
    {
        killComfirmEvent?.Invoke(character);
    }
}
