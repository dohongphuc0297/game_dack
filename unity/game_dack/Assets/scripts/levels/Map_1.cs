using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_1 : MonoBehaviour
{
    public Grid grid;
    public GameObject Cursor;

    public GameObject Lyn = null;
    //private Animator _activeLyn = null;

    private GameStates currentState;
    //private BaseCharacterClass Lyn = new Warrior();
    string str_collider = "not set";
    Ray ray; 
    RaycastHit hit;
    bool Lyn_active = false;
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameStates.PlayerSelectTile;

        //_activeLyn = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //hover
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
        Vector3 pos = new Vector3(coordinate.x, coordinate.y, coordinate.z);
        //Debug.Log(coordinate);
        pos.x += 0.5f;
        pos.y += 0.5f;
        Cursor.transform.position = pos;

        //mouse click
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
               
                //str_collider = hit.collider.name;
            }
        }

        Collider2D coll = Lyn.GetComponent<Collider2D>();

        if(coll.OverlapPoint(mouseWorldPos)) {
            if(!Lyn_active) {
                Lyn_active = true;
            }
        }
        else {
            Lyn_active = false;
        }
        //_activeLyn.SetBool("active", Lyn_active);


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

    void onMouseEnter() {
        Debug.Log("123");
    }

    void OnGUI()
    {
        GUILayout.Label(str_collider);
    }
}
