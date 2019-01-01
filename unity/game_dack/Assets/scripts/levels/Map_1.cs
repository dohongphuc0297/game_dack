using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Map_1 : MonoBehaviour
{
    public Grid grid;
    public GameObject Cursor;
    public GameObject MenuPanel;
    public GameObject InfoPanel;
    public GameObject ActionPanel;
    public GameObject ChangeTurnPanel;

    public Text ChangeTurnText;
    public Text InfoMenuName;
    public Text InfoMenuHP;

    public float speed;

    public Tilemap tilemap;
    public BoundsInt cellBounds;
    public Color color;

    public struct Terrain
    {
        public int type;
        public Vector3Int pos;
        public Terrain(int p1, Vector3Int p2)
        {
            type = p1;
            pos = p2;
        }
    }
    private List<Terrain> terrain = new List<Terrain>();
    
    private List<Vector3Int> moveZone = new List<Vector3Int>();
    private List<Vector3Int> attackZone = new List<Vector3Int>();
    private List<Vector3Int> curAttackZone = new List<Vector3Int>();
    private List<int> MovedUnitIndex = new List<int>();
    private int currentUnitIndex;

    private List<BaseCharacterClass> PlayerUnits = new List<BaseCharacterClass>();
    //public GameObject Lyn = null;
    //private Animator _activeLyn = null;

    private GameStates currentState;
    private bool isClickable = true;
    private bool isHoverable = true;
    private bool isPlayerTurn = false;
    private Vector3 TargetPosition;
    private Vector3 moveTarget;

    private bool changeTurn = false;
    //private BaseCharacterClass Lyn = new Warrior();
    //string str_collider = "not set";

    //bool Lyn_active = false;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(tilemap.GetTile(grid.WorldToCell(new Vector3(-11, 9, 0))));
        for(int x = -18; x<= 18; x++) {
            for(int y = -10; y<=9; y++) {
                string s = tilemap.GetTile(grid.WorldToCell(new Vector3(x, y, 0))).name;
            
                if(s == "grass1" || s == "grass2"){
                    Terrain temp = new Terrain(0, grid.WorldToCell(new Vector3(x, y, 0)));
                    terrain.Add(temp);
                }
                else if(s == "tree1" || s == "tree2" || s == "tree3"){
                    Terrain temp = new Terrain(1, grid.WorldToCell(new Vector3(x, y, 0)));
                    terrain.Add(temp);
                }
                else if(s == "door1"){
                    Terrain temp = new Terrain(2, grid.WorldToCell(new Vector3(x, y, 0)));
                    terrain.Add(temp);
                }
                //else if(s == "house1" || s == "house2" || s == "house3" || s == "house4" || s == "house5" 
                //    || s == "house6" || s == "house7" || s == "house8" || s == "house9") {
                //    Terrain temp = new Terrain(3, grid.WorldToCell(new Vector3(x, y, 0)));
                //    terrain.Add(temp);
                //}
                else {
                    Terrain temp = new Terrain(3, grid.WorldToCell(new Vector3(x, y, 0)));
                    terrain.Add(temp);
                }
            }
        }
        isPlayerTurn = true;
        ChangeTurnPanel.SetActive(false);
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

    public void  PlayChangeTurnPanel()
    {
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(false);
        ActionPanel.SetActive(false);
        ChangeTurnPanel.SetActive(true);
        Animator animate = ChangeTurnText.GetComponent<Animator>();
        animate.SetBool("isActive", true);
        changeTurn = true;
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
    public void BtnMoveClick()
    {
        ColorMoveZone();
        ShowInfoPanel();
        currentState = GameStates.PlayerMoveUnit;
    }
    public void BtnAttackClick()
    {
        //calculate attack zone around unit
        ColorAttackZone();
        currentState = GameStates.PlayerAttackUnit;
    }
    public void BtnEndClick()
    {
        MovedUnitIndex.Add(currentUnitIndex);
        SpriteRenderer spriteR = PlayerUnits[currentUnitIndex]._GameObject.GetComponent<SpriteRenderer>();
        spriteR.color = Color.gray;
        ShowInfoPanel();
        currentState = GameStates.PlayerSelectTile;
    }

    public void BtnEndTurnClick()
    {
        PlayChangeTurnPanel();
    }

    public void BtnCancelClick()
    {
        ShowInfoPanel();
        currentState = GameStates.PlayerSelectTile;
    }

    public void BtnActionCancelClick()
    {
        PlayerUnits[currentUnitIndex]._GameObject.transform.position = TargetPosition;
        ShowInfoPanel();
        currentState = GameStates.PlayerSelectTile;
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

        foreach (Vector3Int pos in attackZone)
        {
            SetTileColour(Color.red, pos, tilemap);
        }
    }

    private void ColorAttackZone()
    {
        foreach (Vector3Int pos in curAttackZone)
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

    private bool IsInAttackZone(Vector3Int point)
    {
        foreach (Vector3Int pos in curAttackZone)
        {
            if (point.x == pos.x && point.y == pos.y) return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(changeTurn == true){
            Animator animate = ChangeTurnText.GetComponent<Animator>();
            if(!animate.GetCurrentAnimatorStateInfo(0).IsName("TurnAnimation")){
                animate.SetBool("isActive", false);
                ChangeTurnPanel.SetActive(false);
                changeTurn = false;
            }
        }
        
        bool haveCharacter = false;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
        if (isPlayerTurn && isHoverable)
        {
            //hover
            Vector3 pos = new Vector3(coordinate.x, coordinate.y, coordinate.z);
            
            pos.x += 0.5f;
            pos.y += 0.5f;
            Cursor.transform.position = pos;
            for (int i = 0; i < PlayerUnits.Count; i++)
            {
                Collider2D coll = PlayerUnits[i]._GameObject.GetComponent<Collider2D>();
                
                if (coll.OverlapPoint(mouseWorldPos))
                {
                    haveCharacter = true;
                    InfoMenuName.text = PlayerUnits[i]._GameObject.name;
                    InfoMenuHP.text = PlayerUnits[i].HP.ToString()+"/"+PlayerUnits[i].HP.ToString();
                    if (MovedUnitIndex.IndexOf(i) >= 0) continue;
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
            if(haveCharacter == false) {
                for(int i = 0; i < terrain.Count; i++) {
                    if(coordinate == terrain[i].pos) {
                        if(terrain[i].type == 0) {
                            InfoMenuName.text = "Grass";
                            InfoMenuHP.text = "Def: 0 / Avoid: 0";
                        }
                        else if(terrain[i].type == 1) {
                            InfoMenuName.text = "Tree";
                            InfoMenuHP.text = "Def: 1 / Avoid: 20";
                        }
                        else if(terrain[i].type == 2) {
                            InfoMenuName.text = "House";
                            InfoMenuHP.text = "Def: 0 / Avoid: 10";
                        }
                        else if(terrain[i].type == 3) {
                            InfoMenuName.text = "Can't move";
                            InfoMenuHP.text = "Def: 0 / Avoid: 0";
                        }
                        break;
                    }
                }
            }
        }

        switch (currentState)
        {
            case GameStates.Start:
                break;
            case GameStates.PlayerSelectTile:
                if (Input.GetMouseButtonDown(0))
                {
                    bool isUnit = false;
                    for (int i = 0; i < PlayerUnits.Count; i++)
                    {
                        if (MovedUnitIndex.IndexOf(i) >= 0)
                        {
                            if (i == PlayerUnits.Count - 1)
                            {
                                ShowMenuPanel();
                                currentState = GameStates.PlayerSelectAction;
                            }
                            continue;
                        }
                        Collider2D coll = PlayerUnits[i]._GameObject.GetComponent<Collider2D>();

                        if (coll.OverlapPoint(mouseWorldPos))
                        {
                            int weaponRange = PlayerUnits[i].EquippedWeapon.Range;    
                            // ket qua tra ve = [1] hoac [1,2] hoac [2] hoac [0] neu khong co vu khi

                            isUnit = true;
                            currentUnitIndex = i;
                            //isHoverable = false;
                            currentState = GameStates.PlayerMoveUnit;
                            //refresh list zone
                            moveZone.Clear();
                            attackZone.Clear();
                            //refresh color of map
                            tilemap.RefreshAllTiles();
                        
                            //calculate array of move zone and attack zone
                            for (int j = coordinate.x - PlayerUnits[i].Movement; j <= coordinate.x + PlayerUnits[i].Movement; j++)
                            {
                                for (int k = coordinate.y - PlayerUnits[i].Movement; k <= coordinate.y + PlayerUnits[i].Movement; k++)
                                {
                                    int t = j - coordinate.x;
                                    if (t < 0) t = -t;
                                    if (k >= coordinate.y - PlayerUnits[i].Movement + t && k <= coordinate.y + PlayerUnits[i].Movement - t)
                                    {
                                        Vector3Int a = new Vector3Int(j, k, 0);
                                        moveZone.Add(a);
                                    }
                                }
                            }
                            //get attack range
                            for (int j = coordinate.x - PlayerUnits[i].Movement; j <= coordinate.x + PlayerUnits[i].Movement; j++)
                            {
                                for (int k = coordinate.y - PlayerUnits[i].Movement; k <= coordinate.y + PlayerUnits[i].Movement; k++)
                                {
                                    int t = j - coordinate.x;
                                    if (t < 0) t = -t;
                                    if (k >= coordinate.y - PlayerUnits[i].Movement + t && k <= coordinate.y + PlayerUnits[i].Movement - t)
                                    {
                                            Vector3Int left = new Vector3Int(j - weaponRange, k, 0);
                                            Vector3Int right = new Vector3Int(j + weaponRange, k, 0);
                                            Vector3Int up = new Vector3Int(j, k + weaponRange, 0);
                                            Vector3Int down = new Vector3Int(j, k - weaponRange, 0);
                                            if (!moveZone.Contains(left))
                                                attackZone.Add(left);
                                            if (!moveZone.Contains(right))
                                                attackZone.Add(right);
                                            if (!moveZone.Contains(up))
                                                attackZone.Add(up);
                                            if (!moveZone.Contains(down))
                                                attackZone.Add(down);
                                    }
                                }
                            }
                            ColorMoveZone();
                        }
                    //ShowActionPanel();
                }
                    if (!isUnit)
                    {
                        ShowMenuPanel();
                        currentState = GameStates.PlayerSelectAction;
                    }
                }
                break;
            case GameStates.PlayerMoveUnit:
                if (Input.GetMouseButtonDown(0))
                {
                    if (IsInMoveZone(coordinate))
                    {
                        //Debug.Log(coordinate);
                        TargetPosition = PlayerUnits[currentUnitIndex]._GameObject.transform.position;
                        moveTarget = new Vector3(coordinate.x + 0.5f, coordinate.y + 0.5f, coordinate.z);
                        currentState = GameStates.UnitMoving;
                    }
                }
                    break;
            case GameStates.UnitMoving:
                PlayerUnits[currentUnitIndex]._GameObject.transform.position = Vector3.MoveTowards(PlayerUnits[currentUnitIndex]._GameObject.transform.position, moveTarget, speed * Time.deltaTime);
                if (PlayerUnits[currentUnitIndex]._GameObject.transform.position == moveTarget)
                {
                    ShowActionPanel();
                    tilemap.RefreshAllTiles();
                    currentState = GameStates.PlayerSelectAction;
                }
                break;
            case GameStates.PlayerSelectAction:
                break;
            case GameStates.PlayerAttackUnit:
                if (Input.GetMouseButtonDown(0))
                {
                    if (IsInAttackZone(coordinate))
                    {
                    }
                }
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
