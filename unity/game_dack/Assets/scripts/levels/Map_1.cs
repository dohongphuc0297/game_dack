using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_1 : MonoBehaviour
{
    public Grid grid;
    public GameObject Cursor;

    private List<BaseCharacterClass> PlayerUnits = new List<BaseCharacterClass>();
    //public GameObject Lyn = null;
    //private Animator _activeLyn = null;

    private GameStates currentState;
    private bool isClickable = true;
    //private BaseCharacterClass Lyn = new Warrior();
    //string str_collider = "not set";
    
    //bool Lyn_active = false;
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameStates.PlayerSelectTile;
        GameObject[] list = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject obj in list)
        {
            switch (obj.name)
            {
                case "Lyn":
                    PlayerUnits.Add(new Warrior(obj));
                    break;
                case "Archer":
                    PlayerUnits.Add(new Archer(obj));
                    break;
                default:
                    break;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
        if (!(currentState == GameStates.PlayerSelectAction) && !(currentState == GameStates.PlayerAttackUnit) && !(currentState == GameStates.EnemyTurn))
        {
            //hover
            Vector3 pos = new Vector3(coordinate.x, coordinate.y, coordinate.z);
            //Debug.Log(coordinate);
            pos.x += 0.5f;
            pos.y += 0.5f;
            Cursor.transform.position = pos;
        }

        //mouse click
        if (isClickable && Input.GetMouseButtonDown(0))
        {
            foreach (BaseCharacterClass unit in PlayerUnits)
            {
                Collider2D coll = unit._GameObject.GetComponent<Collider2D>();

                if (coll.OverlapPoint(mouseWorldPos))
                {
                    if (!(unit.State == CharacterStates.Active))
                    {
                        unit.State = CharacterStates.Active;
                        currentState = GameStates.PlayerSelectAction;
                        unit._Animator.SetBool("isActive", true);
                        //break;
                    }
                }
                else
                {
                    unit.State = CharacterStates.Stance;
                    currentState = GameStates.PlayerSelectTile;
                    unit._Animator.SetBool("isActive", false);
                }
                
            }
        }

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
        GUILayout.Label(currentState.ToString());
    }
}
