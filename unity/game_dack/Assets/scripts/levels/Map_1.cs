using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map_1 : MonoBehaviour
{
    public Grid grid;
    public GameObject Cursor;
    public GameObject MenuPanel;
    public GameObject InfoPanel;
    public GameObject ActionPanel;

    public float speed;

    public Tilemap tilemap;

    private List<Vector3Int> moveZone = new List<Vector3Int>();
    private List<Vector3Int> attackZone = new List<Vector3Int>();
    private int currentUnitIndex;

    private List<BaseCharacterClass> PlayerUnits = new List<BaseCharacterClass>();
    //public GameObject Lyn = null;
    //private Animator _activeLyn = null;

    private GameStates currentState;
    private bool isClickable = true;
    private bool isHoverable = true;
    private bool isMoving = false;
    private Vector3 moveTarget;
    //private BaseCharacterClass Lyn = new Warrior();
    //string str_collider = "not set";

    //bool Lyn_active = false;
    // Start is called before the first frame update
    void Start()
    {
        ShowInfoPanel();
        //tilemap = grid.GetComponent<Tilemap>();
        currentState = GameStates.PlayerSelectTile;
        GameObject[] list = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject obj in list)
        {
            switch (obj.name)
            {
                case "Lyn":
                    PlayerUnits.Add(new Warrior(obj));
                    PlayerUnits[PlayerUnits.Count - 1].EquippedWeapon = new IronSword();
                    break;
                case "Archer":
                    PlayerUnits.Add(new Archer(obj));
                    PlayerUnits[PlayerUnits.Count - 1].EquippedWeapon = new IronBow();
                    break;
                default:
                    break;
            }
            
        }
    }

    public void ShowInfoPanel()
    {
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(true);
        ActionPanel.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        InfoPanel.SetActive(false);
        ActionPanel.SetActive(false);
    }
    public void ShowActionPanel()
    {
        ActionPanel.SetActive(true);
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }
    //source: https://answers.unity.com/questions/1546818/how-can-i-change-a-tile-color-in-unity-by-using-c.html
    /// <summary>
    /// Set the colour of a tile.
    /// </summary>
    /// <param name="colour">The desired colour.</param>
    /// <param name="position">The position of the tile.</param>
    /// <param name="tilemap">The tilemap the tile belongs to.</param>
    private void SetTileColour(Color colour, Vector3Int position, Tilemap tilemap)
    {
        // Flag the tile, inidicating that it can change colour.
        // By default it's set to "Lock Colour".
        tilemap.SetTileFlags(position, TileFlags.None);

        // Set the colour.
        tilemap.SetColor(position, colour);
    }

    private void ColorMoveZone()
    {
        foreach (Vector3Int pos in moveZone)
        {
            SetTileColour(new Color(51, 102, 255, 0.6f), pos, tilemap);
        }
    }

    private void ColorAttackZone()
    {
        foreach (Vector3Int pos in attackZone)
        {
            SetTileColour(Color.red, pos, tilemap);
        }
    }

    private bool IsInMoveZone(Vector3Int point)
    {
        foreach (Vector3Int pos in moveZone)
        {
            if (point.x == pos.x && point.y == pos.y) return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
        if (isHoverable)
        {
            //hover
            Vector3 pos = new Vector3(coordinate.x, coordinate.y, coordinate.z);
            //Debug.Log(coordinate);
            pos.x += 0.5f;
            pos.y += 0.5f;
            Cursor.transform.position = pos;
            for (int i = 0; i < PlayerUnits.Count; i++)
            {
                Collider2D coll = PlayerUnits[i]._GameObject.GetComponent<Collider2D>();

                if (coll.OverlapPoint(mouseWorldPos))
                {
                    if (!(PlayerUnits[i].State == CharacterStates.Active))
                    {
                        PlayerUnits[i].State = CharacterStates.Active;
                        PlayerUnits[i]._Animator.SetBool("isActive", true);
                    }
                }
                else
                {
                    PlayerUnits[i].State = CharacterStates.Stance;
                    PlayerUnits[i]._Animator.SetBool("isActive", false);
                }

            }
        }

        //mouse click
        if (isClickable && Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < PlayerUnits.Count; i++)
            {
                Collider2D coll = PlayerUnits[i]._GameObject.GetComponent<Collider2D>();

                if (coll.OverlapPoint(mouseWorldPos))
                {
                    currentUnitIndex = i;
                    //isHoverable = false;
                    currentState = GameStates.PlayerMoveUnit;
                    //refresh list zone
                    moveZone.Clear();
                    attackZone.Clear();
                    //refresh color of map
                    tilemap.RefreshAllTiles();
                    //calculate array of move zone and attack zone
                    for(int j = coordinate.x - PlayerUnits[i].Movement; j <= coordinate.x + PlayerUnits[i].Movement; j++)
                    {
                        for(int k = coordinate.y - PlayerUnits[i].Movement; k <= coordinate.y + PlayerUnits[i].Movement; k++)
                        {
                            int t = j - coordinate.x;
                            if(t<0) t=-t;
                            if(k>=coordinate.y-PlayerUnits[i].Movement+t && k<=coordinate.y+PlayerUnits[i].Movement-t){
                                Vector3Int a = new Vector3Int(j, k, 0);
                                moveZone.Add(a);
                            }
                        }
                    }
                    //get attack range
                    //PlayerUnits[0].EquippedWeapon.Range         ket qua tra ve = [1] hoac [1,2] hoac [2] hoac [0] neu khong co vu khi
                    
                    /*
                    for(int j = coordinate.x - PlayerUnits[i].AttackRange; j <= coordinate.x + PlayerUnits[i].AttackRange; j++)
                    {
                        for(int k = coordinate.y - PlayerUnits[i].AttackRange; k <= coordinate.y + PlayerUnits[i].AttackRange; k++)
                        {
                            int t = j - coordinate.x;
                            if(t<0) t=-t;
                            if(k>=coordinate.y-PlayerUnits[i].AttackRange+t && k<=coordinate.y+PlayerUnits[i].AttackRange-t){
                                Vector3Int a = new Vector3Int(j, k, 0);
                                SetTileColour(Color.blue, a, tilemap);
                            }
                        }
                    }
                    */
                    //SetTileColour(Color.cyan, coordinate, tilemap);
                    ColorMoveZone();
                    ColorAttackZone();
                    //ShowActionPanel();
                }
                else
                {
                }
                
            }
        }

        switch (currentState)
        {
            case GameStates.Start:
                break;
            case GameStates.PlayerSelectTile:
                break;
            case GameStates.PlayerMoveUnit:
                isClickable = false;
                if (!isMoving && !isClickable && Input.GetMouseButtonDown(0))
                {
                    if (IsInMoveZone(coordinate))
                    {
                        Debug.Log(coordinate);
                        isMoving = true;
                        moveTarget = new Vector3(coordinate.x + 0.5f, coordinate.y + 0.5f, coordinate.z);
                    }
                }
                if (isMoving)
                {
                    PlayerUnits[currentUnitIndex]._GameObject.transform.position = Vector3.MoveTowards(PlayerUnits[currentUnitIndex]._GameObject.transform.position, moveTarget, speed * Time.deltaTime);
                    if (PlayerUnits[currentUnitIndex]._GameObject.transform.position == moveTarget) isMoving = false;
                }
                    break;
            case GameStates.PlayerSelectAction:
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
