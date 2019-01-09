using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Map_1 : MonoBehaviour
{
    public int test = 0;
    public Grid grid;
    public GameObject Cursor;
    public GameObject MenuPanel;
    public GameObject InfoPanel;
    public GameObject ActionPanel;
    public GameObject ChangeTurnPanel;
    public GameObject AttackPanel;
    public GameObject FightWindow;
    public GameObject ExpPanel;
    public GameObject LevelUpAnimation;
    public GameObject StatsUpTable;

    public GameObject PlayerCharacter;
    public GameObject EnemyCharacter;

    public Text ChangeTurnText;
    public Text InfoMenuName;
    public Text InfoMenuHP;
    public Text EnemyHitEffect;
    public Text PlayerHitEffect;

    public float speed;

    //camera
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

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
    public struct UnitInfo {
        public int temp;
        public int Level;
        public float Exp;
        public int MaxHP;
        public int HP;
        public int Strength;
        public int Magic;
        public int Skill;
        public int Speed;
        public int Luck;
        public int Defend;
        public int Resist;
    }

    public struct PlayerInfo {
        public string Name;
        public string Weapon;
        public float Exp;
        public int HP;
        public int MaxHP;
        public int Mt;
        public int Hit;
        public int Crit;
        public int Repeat;
        public void reset(){
            Name = "";
            Weapon = "";
            HP = 0;
            MaxHP = 0;
            Mt = 0;
            Hit = 0;
            Crit = 0;
            Repeat = 0;
        }
    }

    public struct EnemyInfo {
        public string Name;
        public string Weapon;
        public int HP;
        public int MaxHP;
        public int Mt;
        public int Hit;
        public int Crit;
        public int Repeat;
        public void reset(){
            Name = "";
            Weapon = "";
            HP = 0;
            MaxHP = 0;
            Mt = 0;
            Hit = 0;
            Crit = 0;
            Repeat = 0;
        }
    }

    private PlayerInfo playerInfo = new PlayerInfo();
    private EnemyInfo enemyInfo = new EnemyInfo();
    private UnitInfo tempUnit = new UnitInfo();

    private List<Terrain> terrain = new List<Terrain>();
    private List<Sprite> listSprite;
    private List<RuntimeAnimatorController> listController;

    private List<Vector3Int> moveZone = new List<Vector3Int>();
    private List<Vector3Int> attackZone = new List<Vector3Int>();
    private List<Vector3Int> curAttackZone = new List<Vector3Int>();
    private List<int> MovedUnitIndex = new List<int>();
    private int currentUnitIndex;
    private int currentEnemyIndex;
    private int currentEnemyAttackTurn = 0;
    private int currentPlayerAttackTurn = 0;
    private bool isPlayerAttackTurn = true;
    private bool isHit = true;

    private List<BaseCharacterClass> PlayerUnits = new List<BaseCharacterClass>();
    private List<BaseCharacterClass> EnemyUnits = new List<BaseCharacterClass>();
    //public GameObject Lyn = null;
    //private Animator _activeLyn = null;

    private GameStates currentState;
    private bool isHoverable = true;
    private bool isPlayerTurn = false;
    private Vector3 TargetPosition;
    private Vector3 moveTarget;

    private bool changeTurn = false;
    //private BaseCharacterClass Lyn = new Warrior();
    //string str_collider = "not set";

    //bool Lyn_active = false;

    private Text enemyHP;
    private Text playerHP;

    //timer
    float timer = 0;
    bool timerReached = false;

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

        //get sprite list
        Sprite[] sprites = Resources.LoadAll<Sprite>("sprites");
        listSprite = new List<Sprite>(sprites);

        RuntimeAnimatorController[] controllers = Resources.LoadAll<RuntimeAnimatorController>("controllers");
        listController = new List<RuntimeAnimatorController>(controllers);

        MenuPanel.SetActive(false);
        InfoPanel.SetActive(false);
        ActionPanel.SetActive(false);
        AttackPanel.SetActive(false);
        ChangeTurnPanel.SetActive(false);
        FightWindow.SetActive(false);
        ExpPanel.SetActive(false);
        LevelUpAnimation.SetActive(false);
        StatsUpTable.SetActive(false);
        //ShowInfoPanel();
        //tilemap = grid.GetComponent<Tilemap>();
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
                case "Paladin":
                    PlayerUnits.Add(new Paladin(obj));
                    PlayerUnits[PlayerUnits.Count - 1].EquippedWeapon = new IronSword();
                    break;
                case "Knight":
                    PlayerUnits.Add(new Knight(obj));
                    PlayerUnits[PlayerUnits.Count - 1].EquippedWeapon = new IronLance();
                    break;
                case "Summoner":
                    PlayerUnits.Add(new Summoner(obj));
                    PlayerUnits[PlayerUnits.Count - 1].EquippedWeapon = new Flux();
                    break;
                default:
                    break;
            }
        }
        GameObject[] Enemylist = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject obj in Enemylist)
        {
            switch (obj.name)
            {
                case "Brigand":
                    EnemyUnits.Add(new Brigand(obj));
                    EnemyUnits[EnemyUnits.Count - 1].EquippedWeapon = new IronAxe();
                    break;
                case "Brigand1":
                    EnemyUnits.Add(new Brigand(obj));
                    EnemyUnits[EnemyUnits.Count - 1].EquippedWeapon = new IronAxe();
                    break;
                case "Brigand2":
                    EnemyUnits.Add(new Brigand(obj));
                    EnemyUnits[EnemyUnits.Count - 1].EquippedWeapon = new IronAxe();
                    break;
                case "Brigand3":
                    EnemyUnits.Add(new Brigand(obj));
                    EnemyUnits[EnemyUnits.Count - 1].EquippedWeapon = new IronAxe();
                    break;
                case "Brigand4":
                    EnemyUnits.Add(new Brigand(obj));
                    EnemyUnits[EnemyUnits.Count - 1].EquippedWeapon = new IronAxe();
                    break;
                case "Brigand5":
                    EnemyUnits.Add(new Brigand(obj));
                    EnemyUnits[EnemyUnits.Count - 1].EquippedWeapon = new IronAxe();
                    break;
                case "Boss":
                    EnemyUnits.Add(new Brigand(obj));
                    EnemyUnits[EnemyUnits.Count - 1].EquippedWeapon = new IronAxe();
                    break;
                default:
                    break;
            }
        }

        ChangeTurnText.text = "KILL ALL ENEMIES";
        ChangeTurnText.color = Color.red;
        PlayChangeTurnPanel();
        currentState = GameStates.Start;
        isHoverable = false;
        Cursor.SetActive(false);
        isPlayerTurn = false;
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
        AttackPanel.SetActive(false);
        FightWindow.SetActive(false);
    }

    public void HideAllPanel()
    {
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(false);
        ActionPanel.SetActive(false);
        AttackPanel.SetActive(false);
        FightWindow.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        InfoPanel.SetActive(false);
        ActionPanel.SetActive(false);
        AttackPanel.SetActive(false);
    }
    public void ShowActionPanel()
    {
        ActionPanel.SetActive(true);
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(false);
        AttackPanel.SetActive(false);
    }

    public void ShowAttackPanel()
    {
        AttackPanel.SetActive(true);
        ActionPanel.SetActive(false);
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void ShowFightWindow()
    {
        AttackPanel.SetActive(false);
        ActionPanel.SetActive(false);
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(false);
        FightWindow.SetActive(true);
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
        curAttackZone.Clear();
        Vector3Int coordinate = grid.WorldToCell(moveTarget);
        int range = PlayerUnits[currentUnitIndex].EquippedWeapon.Range;
        for (int i = coordinate.x - range; i <= coordinate.x + range; i++)
        {
            for (int j = coordinate.y - range; j <= coordinate.y + range; j++)
            {
                if (i == coordinate.x && j == coordinate.y) continue;
                int t = i - coordinate.x;
                if (t < 0) t = -t;
                if (j >= coordinate.y - range + t && j <= coordinate.y + range - t)
                {
                    Vector3Int a = new Vector3Int(i, j, 0);
                    if (IsPlayerUnit(a) < 0)
                    {
                        curAttackZone.Add(a);
                    }
                }
            }
        }
        ColorAttackZone();
        //ShowInfoPanel();
        currentState = GameStates.PlayerAttackUnit;
    }
    public void BtnEndClick()
    {
        MovedUnitIndex.Add(currentUnitIndex);
        SpriteRenderer spriteR = PlayerUnits[currentUnitIndex]._GameObject.GetComponent<SpriteRenderer>();
        spriteR.color = Color.gray;
        ShowInfoPanel();
        currentState = GameStates.PlayerSelectTile;
        tilemap.RefreshAllTiles();
    }

    public void BtnEndTurnClick()
    {
        RefreshUnitColor();
        ChangeTurnText.text = "ENEMY TURN";
        ChangeTurnText.color = Color.red;
        //Debug.Log(ChangeTurnText.color);
        PlayChangeTurnPanel();
        currentState = GameStates.ToEnemyTurn;
        MovedUnitIndex.Clear();
        currentEnemyIndex = 0;
        isHoverable = false;
        isPlayerTurn = false;
        //Cursor.SetActive(false);
    }

    public void BtnCancelClick()
    {
        ShowInfoPanel();
        currentState = GameStates.PlayerSelectTile;
    }

    public void BtnActionCancelClick()
    {
        tilemap.RefreshAllTiles();
        PlayerUnits[currentUnitIndex]._GameObject.transform.position = TargetPosition;
        ShowInfoPanel();
        currentState = GameStates.PlayerSelectTile;
    }

    public void BtnAttackConfirmClick()
    {
        PlayerCharacter.GetComponent<SpriteRenderer>().sprite = null;
        PlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = null;
        ShowFightWindow();
        Animator animate = FightWindow.GetComponent<Animator>();
        animate.SetTrigger("begin");
        GameObject[] info = GameObject.FindGameObjectsWithTag("BattleInfo");

        foreach (GameObject obj in info)
        {
            switch (obj.name)
            {
                case "HIT_enemy":
                    obj.GetComponent<Text>().text = enemyInfo.Hit.ToString();
                    break;
                case "DMG_enemy":
                    obj.GetComponent<Text>().text = enemyInfo.Mt.ToString();
                    break;
                case "CRT_enemy":
                    obj.GetComponent<Text>().text = enemyInfo.Crit.ToString();
                break;
                case "HP_enemy":
                    enemyHP = obj.GetComponent<Text>();
                    enemyHP.text = enemyInfo.HP.ToString()+"/"+enemyInfo.MaxHP.ToString();
                break;
                case "weapon_enemy":
                    obj.GetComponent<Text>().text = enemyInfo.Weapon;
                break;
                case "EnemyUnitName":
                    obj.GetComponent<Text>().text = enemyInfo.Name;
                break;
                case "HIT_player":
                    obj.GetComponent<Text>().text = playerInfo.Hit.ToString();
                    break;
                case "DMG_player":
                    obj.GetComponent<Text>().text = playerInfo.Mt.ToString();
                    break;
                case "CRT_player":
                    obj.GetComponent<Text>().text = playerInfo.Crit.ToString();
                break;
                case "HP_player":
                    playerHP = obj.GetComponent<Text>();
                    playerHP.text = playerInfo.HP.ToString()+"/"+playerInfo.MaxHP.ToString();
                break;
                case "weapon_player":
                    obj.GetComponent<Text>().text = playerInfo.Weapon;
                break;
                case "PlayerUnitName":
                    obj.GetComponent<Text>().text = playerInfo.Name;
                break;
                default:
                    break;
            }
        }
        
        foreach (Sprite sprite in listSprite)
        {
            if(sprite.name == PlayerUnits[currentUnitIndex]._GameObject.name)
            {
                PlayerCharacter.GetComponent<SpriteRenderer>().sprite = sprite;
                break;
            }
        }
        
        foreach (RuntimeAnimatorController controller in listController)
        {
            if(controller.name == PlayerUnits[currentUnitIndex]._GameObject.name)
            {
                PlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = controller;
                break;
            }
        }
        //set order attack
        if (isPlayerTurn)
        {
            isPlayerAttackTurn = true;
            currentEnemyAttackTurn = 0;
            currentPlayerAttackTurn = 1;
        }
        else
        {
            isPlayerAttackTurn = false;
            currentEnemyAttackTurn = 1;
            currentPlayerAttackTurn = 0;
        }

        currentState = GameStates.AnimationFight;
        //enemyInfo.reset();
        //playerInfo.reset();
    }

    public void BtnAttackCancelClick()
    {
        tilemap.RefreshAllTiles();
        ShowActionPanel();
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
            SetTileColour(new Color(0.2f, 0.4f, 1, 0.8f), pos, tilemap);
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

    private void RefreshUnitColor()
    {
        foreach (BaseCharacterClass unit in PlayerUnits)
        {
            SpriteRenderer spriteR = unit._GameObject.GetComponent<SpriteRenderer>();
            spriteR.color = Color.white;
        }
    }

    private void RefreshEnemyUnitColor()
    {
        foreach (BaseCharacterClass unit in EnemyUnits)
        {
            SpriteRenderer spriteR = unit._GameObject.GetComponent<SpriteRenderer>();
            spriteR.color = Color.white;
        }
    }

    private bool IsOutMap(Vector3Int point)
    {
        if (point.x < -18 || point.x > 18 || point.y < -10 || point.y > 9) return true;
        return false;
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

    private int GetTerrainType(Vector3Int point)
    {
        foreach (Terrain terr in terrain)
        {
            if (point.x == terr.pos.x && point.y == terr.pos.y)
            {
                return terr.type;
            }
        }
        return -1;
    }

    private int IsPlayerUnit(Vector3Int point)
    {
        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            Vector3Int coordinate = grid.WorldToCell(PlayerUnits[i]._GameObject.transform.position);
            if (coordinate.x == point.x && coordinate.y == point.y) return i;
        }
        return -1;
    }

    private int IsEnemyUnit(Vector3Int point)
    {
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            Vector3Int coordinate = grid.WorldToCell(EnemyUnits[i]._GameObject.transform.position);
            if (coordinate.x == point.x && coordinate.y == point.y) return i;
        }
        return -1;
    }

    private bool isMovable(Vector3Int currPos, Vector3Int des, int currStep, int maxStep, string dir)
    {
        if (currStep > maxStep) return false;
        if (!moveZone.Contains(currPos)) return false;
        if (currPos.x == des.x && currPos.y == des.y) return true;
        bool res = false;
        //go left
        if(dir != "right")
        {
            res = res || isMovable(new Vector3Int(currPos.x - 1, currPos.y, 0), des, currStep + 1, maxStep, "left");
        }
        //go right
        if (dir != "left")
        {
            res = res || isMovable(new Vector3Int(currPos.x + 1, currPos.y, 0), des, currStep + 1, maxStep, "right");
        }
        //go up
        if (dir != "down")
        {
            res = res || isMovable(new Vector3Int(currPos.x, currPos.y + 1, 0), des, currStep + 1, maxStep, "up");
        }
        if (dir != "up")
        {
            res = res || isMovable(new Vector3Int(currPos.x, currPos.y - 1, 0), des, currStep + 1, maxStep, "down");
        }
        return res;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerUnits.Count <= 0)
        {
            ChangeTurnText.text = "MAP CLEAR";
            ChangeTurnText.color = Color.blue;
            //Debug.Log(ChangeTurnText.color);
            PlayChangeTurnPanel();
            currentState = GameStates.GameOver;
        }
        if (EnemyUnits.Count <= 0)
        {
            ChangeTurnText.text = "YOU LOSE";
            ChangeTurnText.color = Color.red;
            //Debug.Log(ChangeTurnText.color);
            PlayChangeTurnPanel();
            currentState = GameStates.GameOver;
        }
        Vector3 CameraPos = transform.position;
        if (isPlayerTurn)
        {
            //camera move
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                CameraPos.y += panSpeed * Time.deltaTime;
            }
            else if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                CameraPos.x -= panSpeed * Time.deltaTime;
            }
            else if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                CameraPos.x += panSpeed * Time.deltaTime;
            }
            else if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                CameraPos.y -= panSpeed * Time.deltaTime;
            }
        }
        CameraPos.x = Mathf.Clamp(CameraPos.x, -panLimit.x, panLimit.x);
        CameraPos.y = Mathf.Clamp(CameraPos.y, -panLimit.y, panLimit.y);
        transform.position = CameraPos;

        if (changeTurn == true){
            Animator animate = ChangeTurnText.GetComponent<Animator>();
            if(!animate.GetCurrentAnimatorStateInfo(0).IsName("TurnAnimation")){
                animate.SetBool("isActive", false);
                ChangeTurnPanel.SetActive(false);
                changeTurn = false;
            }
        }
        if(Input.GetMouseButtonDown(1)){
            if(MenuPanel.activeInHierarchy)
            {
                BtnCancelClick();
            }
            if(ActionPanel.activeInHierarchy || currentState == GameStates.PlayerMoveUnit)
            {
                BtnActionCancelClick();
            }
            if(AttackPanel.activeInHierarchy)
            {
                BtnAttackCancelClick();
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
                    InfoMenuHP.text = PlayerUnits[i].HP.ToString()+"/"+PlayerUnits[i].MaxHP.ToString();
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
            if (haveCharacter == false)
            {
                for (int i = 0; i < EnemyUnits.Count; i++)
                {
                    Collider2D coll = EnemyUnits[i]._GameObject.GetComponent<Collider2D>();

                    if (coll.OverlapPoint(mouseWorldPos))
                    {
                        haveCharacter = true;
                        InfoMenuName.text = EnemyUnits[i]._GameObject.name;
                        InfoMenuHP.text = EnemyUnits[i].HP.ToString() + "/" + EnemyUnits[i].MaxHP.ToString();
                    }
                }
            }
            if (haveCharacter == false) {
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
                Vector3 position = PlayerUnits[0]._GameObject.transform.position;
                position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
                position.y = Mathf.Clamp(position.y, -panLimit.y, panLimit.y);
                if (transform.position.x >= position.x - 1 && transform.position.x <= position.x + 1 && transform.position.y >= position.y - 1 && transform.position.y <= position.y + 1)
                {
                    if (!changeTurn)
                    {
                        isHoverable = true;
                        Cursor.SetActive(true);
                        isPlayerTurn = true;
                        ShowInfoPanel();
                        currentState = GameStates.PlayerSelectTile;
                        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                    }
                }
                else
                {
                    float step = speed * 3 * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(Camera.main.transform.position, position, step);
                }
                break;
            case GameStates.PlayerSelectTile:
                if(MovedUnitIndex.Count == PlayerUnits.Count) BtnEndTurnClick();
                if (Input.GetMouseButtonDown(0))
                {
                    if (IsOutMap(coordinate)) break;
                    bool isUnit = false;
                    for (int i = 0; i < PlayerUnits.Count; i++)
                    {
                        if (MovedUnitIndex.IndexOf(i) >= 0)
                        {
                            //if (i == PlayerUnits.Count - 1)
                            //{
                            //    ShowMenuPanel();
                            //    currentState = GameStates.PlayerSelectAction;
                            //}
                            continue;
                        }
                        Collider2D coll = PlayerUnits[i]._GameObject.GetComponent<Collider2D>();

                        if (coll.OverlapPoint(mouseWorldPos))
                        {
                            //get attack range
                            int weaponRange = PlayerUnits[i].EquippedWeapon.Range;

                            isUnit = true;
                            currentUnitIndex = i;
                            //isHoverable = false;
                            currentState = GameStates.PlayerMoveUnit;
                            TargetPosition = PlayerUnits[currentUnitIndex]._GameObject.transform.position;
                            //refresh list zone
                            moveZone.Clear();
                            attackZone.Clear();
                            //refresh color of map
                            tilemap.RefreshAllTiles();

                            //calculate array of move zone and attack zone
                            int MoveRange = PlayerUnits[i].Movement;
                            for (int j = coordinate.x - MoveRange; j <= coordinate.x + MoveRange; j++)
                            {
                                for (int k = coordinate.y - MoveRange; k <= coordinate.y + MoveRange; k++)
                                {
                                    int t = j - coordinate.x;
                                    if (t < 0) t = -t;
                                    if (k >= coordinate.y - MoveRange + t && k <= coordinate.y + MoveRange - t)
                                    {
                                        Vector3Int a = new Vector3Int(j, k, 0);
                                        int type = GetTerrainType(a);
                                        if(type != 3)
                                        {
                                            if (IsEnemyUnit(a) < 0)
                                            {
                                                moveZone.Add(a);
                                            }
                                        }
                                    }
                                }
                            }

                            if(moveZone.Count >= 0)
                            {
                                int index = 0;
                                while (index < moveZone.Count)
                                {
                                    if (moveZone[index].x == coordinate.x && moveZone[index].y == coordinate.y)
                                    {
                                        index++;
                                        continue;
                                    }
                                    if (!isMovable(coordinate, moveZone[index], 0, MoveRange, ""))
                                    {
                                        moveZone.RemoveAt(index);
                                    }
                                    else
                                    {
                                        index++;
                                    }
                                }
                            }
                          
                            //calculate attack zone
                            for(int j = 0; j < moveZone.Count; j++)
                            {
                                for (int l = moveZone[j].x - weaponRange; l <= moveZone[j].x + weaponRange; l++)
                                {
                                    for (int k = moveZone[j].y - weaponRange; k <= moveZone[j].y + weaponRange; k++)
                                    {
                                        int t = l - moveZone[j].x;
                                        if (t < 0) t = -t;
                                        if (k >= moveZone[j].y - weaponRange + t && k <= moveZone[j].y + weaponRange - t)
                                        {
                                            Vector3Int a = new Vector3Int(l, k, 0);
                                            if (!moveZone.Contains(a))
                                            {
                                                if (!attackZone.Contains(a))
                                                {
                                                    attackZone.Add(a);
                                                }
                                            }
                                        }
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
                    Vector3 pos_unit = PlayerUnits[currentUnitIndex]._GameObject.transform.position;
                    Vector3Int pos_unit_int = grid.WorldToCell(pos_unit);
                    if (IsOutMap(coordinate)) break;
                    if (IsInMoveZone(coordinate))
                    {
                        if (IsPlayerUnit(coordinate) < 0 || coordinate == grid.WorldToCell(TargetPosition))
                        {
                            //Debug.Log(coordinate);
                            moveTarget = new Vector3(pos_unit.x + (coordinate.x - pos_unit_int.x), pos_unit.y + (coordinate.y - pos_unit_int.y), coordinate.z);
                            currentState = GameStates.UnitMoving;
                        }
                    }
                }
                break;
            case GameStates.UnitMoving:
                //Camera.main.transform.position = PlayerUnits[currentUnitIndex]._GameObject.transform.position;
                Vector3 temp_moveTarget = moveTarget;
                Vector3 temp_pos_unit = PlayerUnits[currentUnitIndex]._GameObject.transform.position;
                if(Math.Abs(temp_pos_unit.x-temp_moveTarget.x) <= Math.Abs(temp_pos_unit.y-temp_moveTarget.y) && temp_moveTarget == moveTarget){
                    temp_moveTarget.y = temp_pos_unit.y;
                }
                else if(Math.Abs(temp_pos_unit.x-temp_moveTarget.x) > Math.Abs(temp_pos_unit.y-temp_moveTarget.y) && temp_moveTarget == moveTarget)
                {
                    temp_moveTarget.x = temp_pos_unit.x;
                }

                PlayerUnits[currentUnitIndex]._GameObject.transform.position = Vector3.MoveTowards(PlayerUnits[currentUnitIndex]._GameObject.transform.position, temp_moveTarget, speed * Time.deltaTime);
                if (PlayerUnits[currentUnitIndex]._GameObject.transform.position == temp_moveTarget) {
                    PlayerUnits[currentUnitIndex]._GameObject.transform.position = Vector3.MoveTowards(PlayerUnits[currentUnitIndex]._GameObject.transform.position, moveTarget, speed * Time.deltaTime);
                }
                
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
                    if (IsOutMap(coordinate)) break;
                    if (IsInAttackZone(coordinate))
                    {
                        currentEnemyIndex = IsEnemyUnit(coordinate);
                        if (currentEnemyIndex >= 0)
                        {
                            BaseCharacterClass enemy = EnemyUnits[currentEnemyIndex];
                            enemyInfo.MaxHP = enemy.MaxHP;
                            enemyInfo.Name = enemy.CharacterClassName;
                            enemyInfo.Weapon = enemy.EquippedWeapon.WeaponClassName;
                            playerInfo.Exp = PlayerUnits[currentUnitIndex].Exp;
                            playerInfo.MaxHP = PlayerUnits[currentUnitIndex].MaxHP;
                            playerInfo.Name = PlayerUnits[currentUnitIndex].CharacterClassName;
                            playerInfo.Weapon = PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName;
                            ShowAttackPanel();
                            Vector3Int attack_pos = grid.WorldToCell(moveTarget);
                            Vector3Int defend_pos = grid.WorldToCell(coordinate);
                            bool defendCanAttack = false;
                            for (int j = defend_pos.x - enemy.EquippedWeapon.Range; j <= defend_pos.x + enemy.EquippedWeapon.Range; j++)
                            {
                                for (int k = defend_pos.y - enemy.EquippedWeapon.Range; k <= defend_pos.y + enemy.EquippedWeapon.Range; k++)
                                {
                                    int t = j - defend_pos.x;
                                    if (t < 0) t = -t;
                                    if (k >= defend_pos.y - enemy.EquippedWeapon.Range + t && k <= defend_pos.y + enemy.EquippedWeapon.Range - t)
                                    {
                                        Vector3Int a = new Vector3Int(j, k, 0);
                                        if(attack_pos == a) {
                                            defendCanAttack = true;
                                            break;
                                        }
                                    }
                                }
                                if(defendCanAttack == true) break;
                            }

                            int Triangle = 0; //dam+1/Acc+15
                            int typedef = 0;
                            int typeattack = 0;

                            for(int i = 0; i<terrain.Count; i++ ) {
                                if(coordinate == terrain[i].pos) {
                                    typedef = terrain[i].type;
                                }
                                if(attack_pos == terrain[i].pos) {
                                    typeattack = terrain[i].type;
                                }
                            }

                            if(PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Sword")>=0 
                                && enemy.EquippedWeapon.WeaponClassName.IndexOf("Axe")>=0)
                            {
                                Triangle = 1;        
                            }
                            else if(PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Axe")>=0 
                                && enemy.EquippedWeapon.WeaponClassName.IndexOf("Lance")>=0)
                            {
                                Triangle = 1;        
                            }
                            else if(PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Lance")>=0 
                                && enemy.EquippedWeapon.WeaponClassName.IndexOf("Sword")>=0)
                            {
                                Triangle = 1;        
                            }
                            else if(PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Lance")>=0 
                                && enemy.EquippedWeapon.WeaponClassName.IndexOf("Axe")>=0)
                            {
                                Triangle = -1;        
                            }
                            else if(PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Axe")>=0 
                                && enemy.EquippedWeapon.WeaponClassName.IndexOf("Sword")>=0)
                            {
                                Triangle = -1;        
                            }
                            else if(PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Sword")>=0 
                                && enemy.EquippedWeapon.WeaponClassName.IndexOf("Lance")>=0)
                            {
                                Triangle = -1;        
                            }
                            else Triangle = 0;
                            Text[] stats =  FindObjectsOfType<Text>();
                            
                            int terrain_bonus;
                            foreach (Text obj in stats)
                            {
                                switch (obj.name)
                                {
                                    case "PlayerHP":
                                        obj.text = PlayerUnits[currentUnitIndex].HP.ToString();
                                        playerInfo.HP = PlayerUnits[currentUnitIndex].HP;
                                        break;
                                    case "PlayerMt":
                                        //Debug.Log(PlayerUnits[currentUnitIndex].Strength);
                                        //Debug.Log(PlayerUnits[currentUnitIndex].EquippedWeapon.Mt);
                                        //Debug.Log(enemy.Defend);
                                        //Debug.Log(typeattack);
                                        if(typeattack == 1) terrain_bonus = 1;
                                        else terrain_bonus = 0;
                                        int attackMt = PlayerUnits[currentUnitIndex].Strength+PlayerUnits[currentUnitIndex].EquippedWeapon.Mt
                                            - enemy.Defend+Triangle+terrain_bonus;
                                        if(attackMt < 0) attackMt = 0;
                                        obj.text = attackMt.ToString();
                                        playerInfo.Mt = attackMt;
                                        break;
                                    case "PlayerHit":                                        
                                        if(typedef == 1) terrain_bonus = 20;
                                        else if(typedef == 2) terrain_bonus = 10;
                                        else terrain_bonus = 0;
                                        int attackHit = PlayerUnits[currentUnitIndex].EquippedWeapon.Hit+PlayerUnits[currentUnitIndex].Skill*2+
                                            (int)PlayerUnits[currentUnitIndex].Luck/2;
                                        int defendAvoid = enemy.Speed*2+enemy.Luck+terrain_bonus;
                                        int attackaccuracy = attackHit-defendAvoid+Triangle*15;
                                        if(attackaccuracy>100) attackaccuracy = 100;
                                        if(attackaccuracy<0) attackaccuracy = 0;
                                        playerInfo.Hit = attackaccuracy;
                                        obj.text = attackaccuracy.ToString();
                                        break;
                                    case "PlayerCrit":
                                        int attackCrit = PlayerUnits[currentUnitIndex].EquippedWeapon.Crt+(int)PlayerUnits[currentUnitIndex].Skill/2
                                            -enemy.Luck;
                                        if(attackCrit>100) attackCrit = 100;
                                        if(attackCrit<0) attackCrit = 0;
                                        playerInfo.Crit = attackCrit;
                                        obj.text = attackCrit.ToString();
                                        break;
                                    case "PlayerRepeat":
                                        if(PlayerUnits[currentUnitIndex].Speed-enemy.Speed >= 5) {
                                            obj.text = "X2";
                                            playerInfo.Repeat = 2;
                                        }
                                        else {
                                            obj.text = "";
                                            playerInfo.Repeat = 1;
                                        }
                                        break;
                                    case "EnermyHP":
                                        obj.text = enemy.HP.ToString();
                                        enemyInfo.HP = enemy.HP;
                                        break;
                                    case "EnermyMt":
                                        int defendMt;
                                        if(defendCanAttack == true) {
                                            if(typedef == 1) terrain_bonus = 1;
                                            else terrain_bonus = 0;
                                            defendMt = enemy.Strength+enemy.EquippedWeapon.Mt
                                                - PlayerUnits[currentUnitIndex].Defend-Triangle+terrain_bonus;
                                            if(defendMt < 0 ) defendMt = 0;
                                        }                                       
                                        else defendMt = 0;
                                        obj.text = defendMt.ToString();
                                        enemyInfo.Mt = defendMt;
                                        break;
                                    case "EnermyHit":
                                        int defaccuracy; 
                                        if(defendCanAttack == true) {                                      
                                            if(typeattack == 1) terrain_bonus = 20;
                                            else if(typeattack == 2) terrain_bonus = 10;
                                            else terrain_bonus = 0;
                                            int defendHit = enemy.EquippedWeapon.Hit+enemy.Skill*2+
                                                (int)enemy.Luck/2;
                                            int attackAvoid = PlayerUnits[currentUnitIndex].Speed*2+PlayerUnits[currentUnitIndex].Luck+terrain_bonus;
                                            defaccuracy = defendHit-attackAvoid-Triangle*15;
                                            if(defaccuracy>100) defaccuracy = 100;
                                            if(defaccuracy<0) defaccuracy = 0;
                                        }
                                        else defaccuracy = 0;
                                        enemyInfo.Hit = defaccuracy;
                                        obj.text = defaccuracy.ToString();
                                        break;
                                    case "EnermyCrit":
                                        int defendCrit;
                                        if(defendCanAttack == true) {        
                                            defendCrit = enemy.EquippedWeapon.Crt+(int)enemy.Skill/2
                                                - PlayerUnits[currentUnitIndex].Luck;
                                            if(defendCrit>100) defendCrit = 100;
                                            if(defendCrit<0) defendCrit = 0;
                                        }
                                        else defendCrit = 0;
                                        enemyInfo.Crit = defendCrit;
                                        obj.text = defendCrit.ToString();
                                        break;
                                    case "EnermyRepeat":
                                        if(defendCanAttack == true) {
                                            if(enemy.Speed-PlayerUnits[currentUnitIndex].Speed>=5) {
                                                obj.text = "X2";
                                                enemyInfo.Repeat = 2;
                                            }
                                            else {
                                                obj.text = "";
                                                enemyInfo.Repeat = 1;
                                            }
                                        }
                                        else {
                                            obj.text = "";
                                            enemyInfo.Repeat = 0;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            tilemap.RefreshAllTiles();
                            currentState = GameStates.PlayerSelectAction;
                        }
                    }
                }
                break;
            case GameStates.AnimationFight:
                //Debug.Log(PlayerUnits[currentUnitIndex].CharacterClassName);
                Animator FightWindowAnimator = FightWindow.GetComponent<Animator>();
                if (FightWindowAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if(PlayerUnits[currentUnitIndex].HP <= 0)
                    {   
                        Destroy(PlayerUnits[currentUnitIndex]._GameObject);
                        PlayerUnits.RemoveAt(currentUnitIndex);
                        FightWindowAnimator.SetTrigger("playerDeath");
                        currentState = GameStates.AnimationEndFight;
                        Debug.Log("player died");
                        break;
                    }
                    if (EnemyUnits[currentEnemyIndex].HP <= 0)
                    {
                        Destroy(EnemyUnits[currentEnemyIndex]._GameObject);
                        EnemyUnits.RemoveAt(currentEnemyIndex);
                        FightWindowAnimator.SetTrigger("enemyDeath");
                        currentState = GameStates.AnimationEndFight;
                        break;
                    }
                    if ((currentEnemyAttackTurn + currentPlayerAttackTurn) > (playerInfo.Repeat + enemyInfo.Repeat))
                    {
                        FightWindowAnimator.SetTrigger("end");
                        currentState = GameStates.AnimationEndFight;
                    }
                    else
                    {
                        if (isPlayerAttackTurn)
                        {
                            isPlayerAttackTurn = !isPlayerAttackTurn;
                            if (currentPlayerAttackTurn > playerInfo.Repeat) break;
                            Animator playerCharacterAnimator = PlayerCharacter.GetComponent<Animator>();
                            playerCharacterAnimator.SetTrigger("attack");
                            int rand = UnityEngine.Random.Range(0, 100);
                            if (rand <= playerInfo.Hit)
                            {
                                isHit = true;
                                EnemyHitEffect.text = "Hit!";
                            }
                            else
                            {
                                isHit = false;
                                EnemyHitEffect.text = "Miss!";
                            }
                            currentState = GameStates.AnimationPlayerAttack;
                            test++;
                        }
                        else
                        {
                            
                            isPlayerAttackTurn = !isPlayerAttackTurn;
                            if (currentEnemyAttackTurn > enemyInfo.Repeat) break;
                            Animator enemyCharacterAnimator = EnemyCharacter.GetComponent<Animator>();
                            enemyCharacterAnimator.SetTrigger("attack");
                            int rand = UnityEngine.Random.Range(0, 100);
                            if (rand <= enemyInfo.Hit)
                            {
                                isHit = true;
                                PlayerHitEffect.text = "Hit!";
                            }
                            else
                            {
                                isHit = false;
                                PlayerHitEffect.text = "Miss!";
                            }
                            currentState = GameStates.AnimationEnemyAttack;
                        }

                    }
                }
                break;
            case GameStates.AnimationPlayerAttack:
                Animator PlayerCharacterAnimator = PlayerCharacter.GetComponent<Animator>();
                Animator EHA = EnemyHitEffect.GetComponent<Animator>();
                if (PlayerCharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                {
                    //if (isHit && enemyInfo.HP > (EnemyUnits[currentEnemyIndex].HP - playerInfo.Mt))
                    //{
                    //    enemyHP.text = (--enemyInfo.HP).ToString() + "/" + enemyInfo.MaxHP.ToString();
                    //}
                    if (EHA.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        if (isHit)
                        {
                            EnemyUnits[currentEnemyIndex].HP -= playerInfo.Mt;

                            if (EnemyUnits[currentEnemyIndex].HP < 0) {
                                EnemyUnits[currentEnemyIndex].HP = 0;
                                int BossBonus = 0;
                                playerInfo.Exp += (float)Math.Round((double)
                                    ((31+EnemyUnits[currentEnemyIndex].Level-PlayerUnits[currentUnitIndex].Level)/2+
                                    20+BossBonus)*1.8+50, 2);
                            }
                            else playerInfo.Exp += (float)Math.Round((double)
                                (31+EnemyUnits[currentEnemyIndex].Level-PlayerUnits[currentUnitIndex].Level)/2, 2);

                            enemyInfo.HP = EnemyUnits[currentEnemyIndex].HP;
                            enemyHP.text = enemyInfo.HP.ToString() + "/" + enemyInfo.MaxHP.ToString();
                            
                            
                        }
                        EHA.SetBool("isActive", true);
                        if(!isHit) {
                            playerInfo.Exp += 1;
                        }

                    }
                }

                if (PlayerCharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    currentPlayerAttackTurn++;
                    EHA.SetBool("isActive", false);
                    currentState = GameStates.AnimationFight;
                }
                break;
            case GameStates.AnimationEnemyAttack:
                Animator EnemyCharacterAnimator = EnemyCharacter.GetComponent<Animator>();
                Animator PHA = PlayerHitEffect.GetComponent<Animator>();
                if (EnemyCharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                {
                    //if (isHit && playerInfo.HP > (PlayerUnits[currentUnitIndex].HP - enemyInfo.Mt))
                    //{
                    //    playerHP.text = (--playerInfo.HP).ToString() + "/" + playerInfo.MaxHP.ToString();
                    //}
                    if (PHA.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        if (isHit)
                        {
                            PlayerUnits[currentUnitIndex].HP -= enemyInfo.Mt;
                            if (PlayerUnits[currentUnitIndex].HP < 0) PlayerUnits[currentUnitIndex].HP = 0;
                            playerInfo.HP = PlayerUnits[currentUnitIndex].HP;
                            playerHP.text = playerInfo.HP.ToString() + "/" + playerInfo.MaxHP.ToString();
                        }
                        PHA.SetBool("isActive", true);
                    }
                }
                if (EnemyCharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    currentEnemyAttackTurn++;
                    PHA.SetBool("isActive", false);
                    currentState = GameStates.AnimationFight;
                }
                break;
            case GameStates.AnimationEndFight:
                
                Animator fightWindowAnimator = FightWindow.GetComponent<Animator>();
                if (fightWindowAnimator.GetCurrentAnimatorStateInfo(0).IsName("FightStart"))
                {
                    if(playerInfo.HP > 0 && !LevelUpAnimation.activeInHierarchy && !StatsUpTable.activeInHierarchy) 
                    {
                        tempUnit.temp = 0;
                        tempUnit.Level = PlayerUnits[currentUnitIndex].Level;
                        tempUnit.Exp = PlayerUnits[currentUnitIndex].Exp;
                        tempUnit.MaxHP = PlayerUnits[currentUnitIndex].MaxHP;
                        tempUnit.HP = PlayerUnits[currentUnitIndex].HP;
                        tempUnit.Strength = PlayerUnits[currentUnitIndex].Strength;
                        tempUnit.Magic = PlayerUnits[currentUnitIndex].Magic;
                        tempUnit.Skill = PlayerUnits[currentUnitIndex].Skill;
                        tempUnit.Speed = PlayerUnits[currentUnitIndex].Speed;
                        tempUnit.Luck = PlayerUnits[currentUnitIndex].Luck;
                        tempUnit.Defend = PlayerUnits[currentUnitIndex].Defend;
                        tempUnit.Resist = PlayerUnits[currentUnitIndex].Resist;

                        ExpPanel.SetActive(true);

                        //Exp tăng dần và hiện ra màn hình
                        GameObject[] info = GameObject.FindGameObjectsWithTag("ExpIncrease");
                        PlayerUnits[currentUnitIndex].Exp += 1;
                        foreach (GameObject obj in info)
                        {
                            switch (obj.name)
                            {
                                case "Exp":
                                    obj.GetComponent<Text>().text = (Math.Round(tempUnit.Exp)%100).ToString();
                                    break;
                                case "Slider":
                                    obj.GetComponent<Slider>().value = (float)(Math.Round(tempUnit.Exp)%100);
                                    break;
                                default:
                                    break;
                            }
                        }
                        

                        //Khi Exp tăng tới số Exp đã đạt được
                        if(PlayerUnits[currentUnitIndex].Exp >= playerInfo.Exp)
                        {
                            PlayerUnits[currentUnitIndex].Exp = playerInfo.Exp;
                            System.Threading.Thread.Sleep(1000);
                            ExpPanel.SetActive(false);

                            while(PlayerUnits[currentUnitIndex].Exp >= 100) {
                                PlayerUnits[currentUnitIndex].Level += 1;
                                int increase_limit = 0;
                                int rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRHP && increase_limit <= 4) {
                                    PlayerUnits[currentUnitIndex].MaxHP+=1;
                                    PlayerUnits[currentUnitIndex].HP+=1;
                                    increase_limit++;
                                }
                                rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRStrength && increase_limit<=4) {
                                    PlayerUnits[currentUnitIndex].Strength++;
                                    increase_limit++;
                                }
                                rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRMagic && increase_limit<=4) {
                                    PlayerUnits[currentUnitIndex].Magic++;
                                    increase_limit++;
                                }
                                rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRSkill && increase_limit<=4) {
                                    PlayerUnits[currentUnitIndex].Skill++;
                                    increase_limit++;
                                }
                                rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRSpeed && increase_limit<=4) {
                                    PlayerUnits[currentUnitIndex].Speed++;
                                    increase_limit++;
                                }
                                rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRLuck && increase_limit<=4) {
                                    PlayerUnits[currentUnitIndex].Luck++;
                                    increase_limit++;
                                }
                                rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRDefend && increase_limit<=4) {
                                    PlayerUnits[currentUnitIndex].Defend++;
                                    increase_limit++;
                                }
                                rand = UnityEngine.Random.Range(1, 101);
                                if(rand <= PlayerUnits[currentUnitIndex].GRResist && increase_limit<=4) {
                                    PlayerUnits[currentUnitIndex].Resist++;
                                    increase_limit++;
                                }
                                PlayerUnits[currentUnitIndex].Exp-=100;
                                LevelUpAnimation.SetActive(true);
                            }
                            playerInfo.Exp = PlayerUnits[currentUnitIndex].Exp;
                        }
                    }   

                    if(!ExpPanel.activeInHierarchy)
                    {
                        if(playerInfo.HP>0) {
                            if(PlayerUnits[currentUnitIndex].Level != tempUnit.Level) {
                                Animator LevelUpAnimator = LevelUpAnimation.GetComponent<Animator>();
                                if (LevelUpAnimator.GetCurrentAnimatorStateInfo(0).IsName("End")){
                                    LevelUpAnimation.SetActive(false);
                                    StatsUpTable.SetActive(true);
                                }
                            }
                        }
                        
                        if(!LevelUpAnimation.activeInHierarchy) {
                            if(StatsUpTable.activeInHierarchy) 
                            {
                                GameObject[] info = GameObject.FindGameObjectsWithTag("StatsUp");
                                int check = 0;
                                foreach (GameObject obj in info)
                                {
                                    switch (obj.name)
                                    {
                                        case "Name":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].CharacterClassName;
                                            break;
                                        case "Level":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Level.ToString();
                                            break;
                                        case "LevelUp":
                                            obj.GetComponent<Text>().text = "+"+(PlayerUnits[currentUnitIndex].Level - tempUnit.Level).ToString();
                                            break;
                                        case "HP":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].MaxHP.ToString();
                                            break;
                                        case "SliderHP":
                                            if(tempUnit.MaxHP < PlayerUnits[currentUnitIndex].MaxHP) {
                                                if(tempUnit.temp < PlayerUnits[currentUnitIndex].MaxHP) {
                                                    check++;
                                                    obj.GetComponent<Slider>().value = tempUnit.temp;
                                                }
                                            }
                                            else obj.GetComponent<Slider>().value = 0;
                                            break;
                                        case "HPUp":
                                            if(tempUnit.MaxHP < PlayerUnits[currentUnitIndex].MaxHP && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].MaxHP)
                                            {
                                                obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].MaxHP-tempUnit.MaxHP).ToString();
                                            }
                                            else obj.GetComponent<Text>().text = "";
                                            break;    
                                        case "Str":
                                            if(PlayerUnits[currentUnitIndex].Strength == 0) {
                                                obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Magic.ToString();
                                            }
                                            else obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Strength.ToString();
                                            break;
                                        case "SliderStr":
                                            if(PlayerUnits[currentUnitIndex].Strength == 0) {
                                                if(tempUnit.Magic < PlayerUnits[currentUnitIndex].Magic) 
                                                {
                                                    if(tempUnit.temp < PlayerUnits[currentUnitIndex].Magic) 
                                                    {
                                                        check++;
                                                        obj.GetComponent<Slider>().value = tempUnit.temp;
                                                    }
                                                }
                                                else obj.GetComponent<Slider>().value = 0;
                                            }
                                            else {
                                                if(tempUnit.Strength < PlayerUnits[currentUnitIndex].Strength) 
                                                {
                                                    if(tempUnit.temp < PlayerUnits[currentUnitIndex].Strength) 
                                                    {
                                                        check++;
                                                        obj.GetComponent<Slider>().value = tempUnit.temp;
                                                    }
                                                }
                                                else obj.GetComponent<Slider>().value = 0;
                                            }
                                            
                                            break;
                                        case "StrUp":
                                            if(PlayerUnits[currentUnitIndex].Strength == 0) {
                                                if(tempUnit.Magic < PlayerUnits[currentUnitIndex].Magic && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].Magic)
                                                {
                                                    obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].Magic-tempUnit.Magic).ToString();
                                                }
                                                else obj.GetComponent<Text>().text = "";
                                            }
                                            else {
                                                if(tempUnit.Strength < PlayerUnits[currentUnitIndex].Strength && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].Strength)
                                                {
                                                    obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].Strength-tempUnit.Strength).ToString();
                                                }
                                                else obj.GetComponent<Text>().text = "";
                                            }
                                            break;    
                                        case "Skill":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Skill.ToString();
                                            break;
                                        case "SliderSkill":
                                            if(tempUnit.Skill < PlayerUnits[currentUnitIndex].Skill) {
                                                if(tempUnit.temp < PlayerUnits[currentUnitIndex].Skill) {
                                                    check++;
                                                    obj.GetComponent<Slider>().value = tempUnit.temp;
                                                }
                                            }
                                            else obj.GetComponent<Slider>().value = 0;
                                            break;
                                        case "SkillUp":
                                            if(tempUnit.Skill < PlayerUnits[currentUnitIndex].Skill && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].Skill)
                                            {
                                                obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].Skill-tempUnit.Skill).ToString();
                                            }
                                            else obj.GetComponent<Text>().text = "";
                                            break;    
                                        case "Spd":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Speed.ToString();
                                            break;
                                        case "SliderSpd":
                                            if(tempUnit.Speed < PlayerUnits[currentUnitIndex].Speed) {
                                                if(tempUnit.temp < PlayerUnits[currentUnitIndex].Speed) {
                                                    check++;
                                                    obj.GetComponent<Slider>().value = tempUnit.temp;
                                                }
                                            }
                                            else obj.GetComponent<Slider>().value = 0;
                                            break;
                                        case "SpdUp":
                                            if(tempUnit.Speed < PlayerUnits[currentUnitIndex].Speed && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].Speed)
                                            {
                                                obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].Speed-tempUnit.Speed).ToString();
                                            }
                                            else obj.GetComponent<Text>().text = "";
                                            break;  
                                        case "Luck":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Luck.ToString();
                                            break;
                                        case "SliderLuck":
                                            if(tempUnit.Luck < PlayerUnits[currentUnitIndex].Luck) {
                                                if(tempUnit.temp < PlayerUnits[currentUnitIndex].Luck) {
                                                    check++;
                                                    obj.GetComponent<Slider>().value = tempUnit.temp;
                                                }
                                            }
                                            else obj.GetComponent<Slider>().value = 0;
                                            break;
                                        case "LuckUp":
                                            if(tempUnit.Luck < PlayerUnits[currentUnitIndex].Luck && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].Luck)
                                            {
                                                obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].Luck-tempUnit.Luck).ToString();
                                            }
                                            else obj.GetComponent<Text>().text = "";
                                            break;    
                                        case "Def":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Defend.ToString();
                                            break;
                                        case "SliderDef":
                                            if(tempUnit.Defend < PlayerUnits[currentUnitIndex].Defend) {
                                                if(tempUnit.temp < PlayerUnits[currentUnitIndex].Defend) {
                                                    check++;
                                                    obj.GetComponent<Slider>().value = tempUnit.temp;
                                                }
                                            }
                                            else obj.GetComponent<Slider>().value = 0;
                                            break;
                                        case "DefUp":
                                            if(tempUnit.Defend < PlayerUnits[currentUnitIndex].Defend && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].Defend)
                                            {
                                                obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].Defend-tempUnit.Defend).ToString();
                                            }
                                            else obj.GetComponent<Text>().text = "";
                                            break;    
                                        case "Res":
                                            obj.GetComponent<Text>().text = PlayerUnits[currentUnitIndex].Resist.ToString();
                                            break;
                                        case "SliderRes":
                                            if(tempUnit.Resist < PlayerUnits[currentUnitIndex].Resist) {
                                                if(tempUnit.temp < PlayerUnits[currentUnitIndex].Resist) {
                                                    check++;
                                                    obj.GetComponent<Slider>().value = tempUnit.temp;
                                                }
                                            }
                                            else obj.GetComponent<Slider>().value = 0;
                                            break;
                                        case "ResUp":
                                            if(tempUnit.Resist < PlayerUnits[currentUnitIndex].Resist && tempUnit.temp+1 >= PlayerUnits[currentUnitIndex].Resist)
                                            {
                                                obj.GetComponent<Text>().text = "+" + (PlayerUnits[currentUnitIndex].Resist-tempUnit.Resist).ToString();
                                            }
                                            else obj.GetComponent<Text>().text = "";
                                            break;    
                                        default:
                                            break;
                                    }
                                }
                                tempUnit.temp+=1;
                                if(check == 0) {
                                    System.Threading.Thread.Sleep(2000);
                                    StatsUpTable.SetActive(false);
                                }
                            }

                            if (!StatsUpTable.activeInHierarchy)
                            {
                                if (isPlayerTurn)
                                {
                                    if (playerInfo.HP > 0)
                                    {
                                        MovedUnitIndex.Add(currentUnitIndex);
                                        SpriteRenderer spriteR = PlayerUnits[currentUnitIndex]._GameObject.GetComponent<SpriteRenderer>();
                                        spriteR.color = Color.gray;
                                    }
                                    ShowInfoPanel();
                                    currentState = GameStates.PlayerSelectTile;
                                }
                                else
                                {
                                    if(enemyInfo.HP > 0)
                                    {
                                        MovedUnitIndex.Add(currentEnemyIndex);
                                        SpriteRenderer spriteR = EnemyUnits[currentEnemyIndex]._GameObject.GetComponent<SpriteRenderer>();
                                        spriteR.color = Color.gray;
                                    }
                                    currentEnemyIndex++;
                                    HideAllPanel();
                                    currentState = GameStates.ToEnemyTurn;
                                }
                            }
                        }
                        
                    }
                }
                    break;
            case GameStates.AnimationPlayerDeath:
                break;
            case GameStates.AnimationEnemyDeath:
                break;
            case GameStates.ToEnemyTurn:
                if(currentEnemyIndex >= EnemyUnits.Count)
                {
                    currentState = GameStates.EnemyTurn;
                }
                else
                {
                    Vector3 pos = EnemyUnits[currentEnemyIndex]._GameObject.transform.position;
                    pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
                    pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
                    if (transform.position.x >= pos.x - 1 && transform.position.x <= pos.x + 1 && transform.position.y >= pos.y - 1 && transform.position.y <= pos.y + 1)
                    {
                        if (!changeTurn)
                        {
                            currentState = GameStates.EnemyTurn;
                            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                        }
                    }
                    else
                    {
                        float step = speed * 3 * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(Camera.main.transform.position, pos, step);
                    }
                }
                break;
            case GameStates.EnemyTurn:
                if (MovedUnitIndex.Count >= EnemyUnits.Count)
                {
                    RefreshEnemyUnitColor();
                    ChangeTurnText.text = "YOUR TURN";
                    ChangeTurnText.color = Color.blue;
                    MovedUnitIndex.Clear();
                    PlayChangeTurnPanel();
                    InfoPanel.SetActive(true);
                    currentState = GameStates.Start;
                    //isPlayerTurn = true;
                    //isHoverable = true;
                    currentEnemyIndex = EnemyUnits.Count - 1;
                    currentUnitIndex = PlayerUnits.Count - 1;
                    //Cursor.SetActive(true);
                }
                else
                {
                    coordinate = grid.WorldToCell(EnemyUnits[currentEnemyIndex]._GameObject.transform.position);
                    if (EnemyUnits[currentEnemyIndex]._GameObject.name == "Boss")
                    {
                        curAttackZone.Clear();
                        int range = EnemyUnits[currentEnemyIndex].EquippedWeapon.Range;
                        for (int i = coordinate.x - range; i <= coordinate.x + range; i++)
                        {
                            for (int j = coordinate.y - range; j <= coordinate.y + range; j++)
                            {
                                if (i == coordinate.x && j == coordinate.y) continue;
                                int t = i - coordinate.x;
                                if (t < 0) t = -t;
                                if (j >= coordinate.y - range + t && j <= coordinate.y + range - t)
                                {
                                    Vector3Int a = new Vector3Int(i, j, 0);
                                    if (IsEnemyUnit(a) < 0)
                                    {
                                        curAttackZone.Add(a);
                                    }
                                }
                            }
                        }
                        ColorAttackZone();
                        timer = 0;
                        timerReached = false;
                        currentState = GameStates.EnemyUnitChooseAttack;
                        break;
                    }
                    //Camera.main.transform.position = EnemyUnits[currentEnemyIndex]._GameObject.transform.position;
                    int weaponRange = EnemyUnits[currentEnemyIndex].EquippedWeapon.Range;
                    //refresh list zone
                    moveZone.Clear();
                    attackZone.Clear();
                    //calculate array of move zone and attack zone
                    int MoveRange = EnemyUnits[currentEnemyIndex].Movement;
                    for (int j = coordinate.x - MoveRange; j <= coordinate.x + MoveRange; j++)
                    {
                        for (int k = coordinate.y - MoveRange; k <= coordinate.y + MoveRange; k++)
                        {
                            int t = j - coordinate.x;
                            if (t < 0) t = -t;
                            if (k >= coordinate.y - MoveRange + t && k <= coordinate.y + MoveRange - t)
                            {
                                Vector3Int a = new Vector3Int(j, k, 0);
                                int type = GetTerrainType(a);
                                if (type != 3)
                                {
                                    if (IsPlayerUnit(a) < 0)
                                    {
                                        moveZone.Add(a);
                                    }
                                }
                            }
                        }
                    }
                    
                    if (moveZone.Count >= 0)
                    {
                        int index = 0;
                        while (index < moveZone.Count)
                        {
                            if (moveZone[index].x == coordinate.x && moveZone[index].y == coordinate.y)
                            {
                                index++;
                                continue;
                            }
                            if (!isMovable(coordinate, moveZone[index], 0, MoveRange, ""))
                            {
                                moveZone.RemoveAt(index);
                            }
                            else
                            {
                                index++;
                            }
                        }
                    }
                    
                    //calculate attack zone
                    for (int j = 0; j < moveZone.Count; j++)
                    {
                        for (int l = moveZone[j].x - weaponRange; l <= moveZone[j].x + weaponRange; l++)
                        {
                            for (int k = moveZone[j].y - weaponRange; k <= moveZone[j].y + weaponRange; k++)
                            {
                                int t = l - moveZone[j].x;
                                if (t < 0) t = -t;
                                if (k >= moveZone[j].y - weaponRange + t && k <= moveZone[j].y + weaponRange - t)
                                {
                                    Vector3Int a = new Vector3Int(l, k, 0);
                                    if (!moveZone.Contains(a))
                                    {
                                        if (!attackZone.Contains(a))
                                        {
                                            attackZone.Add(a);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ColorMoveZone();
                    int NerestPlayerUnitIndex = 0;
                    float currDis = Vector3.Distance(PlayerUnits[0]._GameObject.transform.position, EnemyUnits[currentEnemyIndex]._GameObject.transform.position);
                    for (int i = 1; i < PlayerUnits.Count; i++)
                    {
                        float Dis = Vector3.Distance(PlayerUnits[i]._GameObject.transform.position, EnemyUnits[currentEnemyIndex]._GameObject.transform.position);
                        if (currDis > Dis)
                        {
                            currDis = Dis;
                            NerestPlayerUnitIndex = i;
                        }
                    }
                    Vector3Int coorTemp = grid.WorldToCell(PlayerUnits[NerestPlayerUnitIndex]._GameObject.transform.position);
                    Vector3 pos_unit = EnemyUnits[currentEnemyIndex]._GameObject.transform.position;
                    Vector3Int pos_unit_int = grid.WorldToCell(pos_unit);
                    currDis = Vector3Int.Distance(coorTemp, moveZone[0]);
                    moveTarget = new Vector3(pos_unit.x + (moveZone[0].x - pos_unit_int.x), pos_unit.y + (moveZone[0].y - pos_unit_int.y), moveZone[0].z);
                    for (int i = 0; i < moveZone.Count; i++)
                    {
                        if(IsEnemyUnit(moveZone[i]) < 0)
                        {
                            float Dis = Vector3Int.Distance(coorTemp, moveZone[i]);
                            if (currDis > Dis)
                            {
                                currDis = Dis;
                                moveTarget = new Vector3(pos_unit.x + (moveZone[i].x - pos_unit_int.x), pos_unit.y + (moveZone[i].y - pos_unit_int.y), moveZone[i].z);
                            }
                        }
                    }
                    Cursor.transform.position = moveTarget;
                    currentState = GameStates.EnemyUnitMoving;
                }
                break;
            case GameStates.EnemyUnitMoving:
                //Camera.main.transform.position = EnemyUnits[currentEnemyIndex]._GameObject.transform.position;
                Vector3 temp_moveTargetEnemy = moveTarget;
                Vector3 temp_pos_unitEnemy = EnemyUnits[currentEnemyIndex]._GameObject.transform.position;
                if (Math.Abs(temp_pos_unitEnemy.x - temp_moveTargetEnemy.x) <= Math.Abs(temp_pos_unitEnemy.y - temp_moveTargetEnemy.y) && temp_moveTargetEnemy == moveTarget)
                {
                    temp_moveTargetEnemy.y = temp_pos_unitEnemy.y;
                }
                else if (Math.Abs(temp_pos_unitEnemy.x - temp_moveTargetEnemy.x) > Math.Abs(temp_pos_unitEnemy.y - temp_moveTargetEnemy.y) && temp_moveTargetEnemy == moveTarget)
                {
                    temp_moveTargetEnemy.x = temp_pos_unitEnemy.x;
                }

                EnemyUnits[currentEnemyIndex]._GameObject.transform.position = Vector3.MoveTowards(EnemyUnits[currentEnemyIndex]._GameObject.transform.position, temp_moveTargetEnemy, speed * Time.deltaTime);
                if (EnemyUnits[currentEnemyIndex]._GameObject.transform.position == temp_moveTargetEnemy)
                {
                    EnemyUnits[currentEnemyIndex]._GameObject.transform.position = Vector3.MoveTowards(EnemyUnits[currentEnemyIndex]._GameObject.transform.position, moveTarget, speed * Time.deltaTime);
                }

                if (EnemyUnits[currentEnemyIndex]._GameObject.transform.position == moveTarget)
                {
                    tilemap.RefreshAllTiles();
                    //MovedUnitIndex.Add(currentEnemyIndex);
                    //currentEnemyIndex++;
                    curAttackZone.Clear();
                    coordinate = grid.WorldToCell(moveTarget);
                    int range = EnemyUnits[currentEnemyIndex].EquippedWeapon.Range;
                    for (int i = coordinate.x - range; i <= coordinate.x + range; i++)
                    {
                        for (int j = coordinate.y - range; j <= coordinate.y + range; j++)
                        {
                            if (i == coordinate.x && j == coordinate.y) continue;
                            int t = i - coordinate.x;
                            if (t < 0) t = -t;
                            if (j >= coordinate.y - range + t && j <= coordinate.y + range - t)
                            {
                                Vector3Int a = new Vector3Int(i, j, 0);
                                if (IsEnemyUnit(a) < 0)
                                {
                                    curAttackZone.Add(a);
                                }
                            }
                        }
                    }
                    ColorAttackZone();
                    timer = 0;
                    timerReached = false;
                    currentState = GameStates.EnemyUnitChooseAttack;
                }
                break;
            case GameStates.EnemyUnitChooseAttack:
                if (!timerReached) timer += Time.deltaTime;

                if (!timerReached && timer > 1)
                {
                    currentState = GameStates.EnemyUnitAttack;
                    timerReached = true;
                }
                break;
            case GameStates.EnemyUnitAttack:
                bool hasPlayerUnit = false;
                for (int i = 0; i < curAttackZone.Count; i++)
                {
                    int index = IsPlayerUnit(curAttackZone[i]);
                    if (index >= 0)
                    {
                        hasPlayerUnit = true;
                        Vector3 pos = new Vector3(moveTarget.x, moveTarget.y, moveTarget.z);
                        pos.x += 0.5f;
                        pos.y += 0.5f;
                        Cursor.transform.position = pos;
                        //System.Threading.Thread.Sleep(1000);
                        currentUnitIndex = index;
                        BaseCharacterClass enemy = EnemyUnits[currentEnemyIndex];
                        enemyInfo.MaxHP = enemy.MaxHP;
                        enemyInfo.Name = enemy.CharacterClassName;
                        enemyInfo.Weapon = enemy.EquippedWeapon.WeaponClassName;
                        playerInfo.Exp = PlayerUnits[currentUnitIndex].Exp;
                        playerInfo.MaxHP = PlayerUnits[currentUnitIndex].MaxHP;
                        playerInfo.Name = PlayerUnits[currentUnitIndex].CharacterClassName;
                        playerInfo.Weapon = PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName;
                        ShowAttackPanel();
                        Vector3Int attack_pos = grid.WorldToCell(moveTarget);
                        Vector3Int defend_pos = grid.WorldToCell(curAttackZone[i]);
                        bool defendCanAttack = false;
                        int range = PlayerUnits[currentUnitIndex].EquippedWeapon.Range;
                        for (int j = defend_pos.x - range; j <= defend_pos.x + range; j++)
                        {
                            for (int k = defend_pos.y - range; k <= defend_pos.y + range; k++)
                            {
                                int t = j - defend_pos.x;
                                if (t < 0) t = -t;
                                if (k >= defend_pos.y - range + t && k <= defend_pos.y + range - t)
                                {
                                    Vector3Int a = new Vector3Int(j, k, 0);
                                    if (attack_pos == a)
                                    {
                                        defendCanAttack = true;
                                        break;
                                    }
                                }
                            }
                            if (defendCanAttack == true) break;
                        }

                        int Triangle = 0; //dam+1/Acc+15
                        int typedef = 0;
                        int typeattack = 0;

                        for (int j = 0; j < terrain.Count; j++)
                        {
                            if (coordinate == terrain[j].pos)
                            {
                                typedef = terrain[j].type;
                            }
                            if (attack_pos == terrain[j].pos)
                            {
                                typeattack = terrain[j].type;
                            }
                        }

                        if (PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Sword") >= 0
                            && enemy.EquippedWeapon.WeaponClassName.IndexOf("Axe") >= 0)
                        {
                            Triangle = 1;
                        }
                        else if (PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Axe") >= 0
                            && enemy.EquippedWeapon.WeaponClassName.IndexOf("Lance") >= 0)
                        {
                            Triangle = 1;
                        }
                        else if (PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Lance") >= 0
                            && enemy.EquippedWeapon.WeaponClassName.IndexOf("Sword") >= 0)
                        {
                            Triangle = 1;
                        }
                        else if (PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Lance") >= 0
                            && enemy.EquippedWeapon.WeaponClassName.IndexOf("Axe") >= 0)
                        {
                            Triangle = -1;
                        }
                        else if (PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Axe") >= 0
                            && enemy.EquippedWeapon.WeaponClassName.IndexOf("Sword") >= 0)
                        {
                            Triangle = -1;
                        }
                        else if (PlayerUnits[currentUnitIndex].EquippedWeapon.WeaponClassName.IndexOf("Sword") >= 0
                            && enemy.EquippedWeapon.WeaponClassName.IndexOf("Lance") >= 0)
                        {
                            Triangle = -1;
                        }
                        else Triangle = 0;
                        Text[] stats = FindObjectsOfType<Text>();

                        int terrain_bonus;
                        foreach (Text obj in stats)
                        {
                            switch (obj.name)
                            {
                                case "PlayerHP":
                                    obj.text = PlayerUnits[currentUnitIndex].HP.ToString();
                                    playerInfo.HP = PlayerUnits[currentUnitIndex].HP;
                                    break;
                                case "PlayerMt":
                                    //Debug.Log(PlayerUnits[currentUnitIndex].Strength);
                                    //Debug.Log(PlayerUnits[currentUnitIndex].EquippedWeapon.Mt);
                                    //Debug.Log(enemy.Defend);
                                    //Debug.Log(typeattack);
                                    int attackMt;
                                    if(defendCanAttack == true)
                                    {
                                        if (typeattack == 1) terrain_bonus = 1;
                                        else terrain_bonus = 0;
                                        attackMt = PlayerUnits[currentUnitIndex].Strength + PlayerUnits[currentUnitIndex].EquippedWeapon.Mt
                                            - enemy.Defend + Triangle + terrain_bonus;
                                        if (attackMt < 0) attackMt = 0;
                                    }
                                    else
                                    {
                                        attackMt = 0;
                                    }
                                    obj.text = attackMt.ToString();
                                    playerInfo.Mt = attackMt;
                                    break;
                                case "PlayerHit":
                                    int attackaccuracy;
                                    if (defendCanAttack == true)
                                    {
                                        if (typedef == 1) terrain_bonus = 20;
                                        else if (typedef == 2) terrain_bonus = 10;
                                        else terrain_bonus = 0;
                                        int attackHit = PlayerUnits[currentUnitIndex].EquippedWeapon.Hit + PlayerUnits[currentUnitIndex].Skill * 2 +
                                            (int)PlayerUnits[currentUnitIndex].Luck / 2;
                                        int defendAvoid = enemy.Speed * 2 + enemy.Luck + terrain_bonus;
                                        attackaccuracy = attackHit - defendAvoid + Triangle * 15;
                                        if (attackaccuracy > 100) attackaccuracy = 100;
                                        if (attackaccuracy < 0) attackaccuracy = 0;
                                    }
                                    else attackaccuracy = 0;
                                    playerInfo.Hit = attackaccuracy;
                                    obj.text = attackaccuracy.ToString();
                                    break;
                                case "PlayerCrit":
                                    int attackCrit;
                                    if (defendCanAttack == true)
                                    {
                                        attackCrit = PlayerUnits[currentUnitIndex].EquippedWeapon.Crt + (int)PlayerUnits[currentUnitIndex].Skill / 2
                                        - enemy.Luck;
                                        if (attackCrit > 100) attackCrit = 100;
                                        if (attackCrit < 0) attackCrit = 0;
                                    }
                                    else attackCrit = 0;
                                    playerInfo.Crit = attackCrit;
                                    obj.text = attackCrit.ToString();
                                    break;
                                case "PlayerRepeat":
                                    if (defendCanAttack == true)
                                    {
                                        if (PlayerUnits[currentUnitIndex].Speed - enemy.Speed >= 5)
                                        {
                                            obj.text = "X2";
                                            playerInfo.Repeat = 2;
                                        }
                                        else
                                        {
                                            obj.text = "";
                                            playerInfo.Repeat = 1;
                                        }
                                    }
                                    else
                                    {
                                        obj.text = "";
                                        playerInfo.Repeat = 0;
                                    }
                                    break;
                                case "EnermyHP":
                                    obj.text = enemy.HP.ToString();
                                    enemyInfo.HP = enemy.HP;
                                    break;
                                case "EnermyMt":
                                    int defendMt;
                                    if (typedef == 1) terrain_bonus = 1;
                                    else terrain_bonus = 0;
                                    defendMt = enemy.Strength + enemy.EquippedWeapon.Mt
                                        - PlayerUnits[currentUnitIndex].Defend - Triangle + terrain_bonus;
                                    if (defendMt < 0) defendMt = 0;
                                    obj.text = defendMt.ToString();
                                    enemyInfo.Mt = defendMt;
                                    break;
                                case "EnermyHit":
                                    int defaccuracy;
                                    if (typeattack == 1) terrain_bonus = 20;
                                    else if (typeattack == 2) terrain_bonus = 10;
                                    else terrain_bonus = 0;
                                    int defendHit = enemy.EquippedWeapon.Hit + enemy.Skill * 2 +
                                        (int)enemy.Luck / 2;
                                    int attackAvoid = PlayerUnits[currentUnitIndex].Speed * 2 + PlayerUnits[currentUnitIndex].Luck + terrain_bonus;
                                    defaccuracy = defendHit - attackAvoid - Triangle * 15;
                                    if (defaccuracy > 100) defaccuracy = 100;
                                    if (defaccuracy < 0) defaccuracy = 0;
                                    enemyInfo.Hit = defaccuracy;
                                    obj.text = defaccuracy.ToString();
                                    break;
                                case "EnermyCrit":
                                    int defendCrit;
                                    defendCrit = enemy.EquippedWeapon.Crt + (int)enemy.Skill / 2
                                            - PlayerUnits[currentUnitIndex].Luck;
                                    if (defendCrit > 100) defendCrit = 100;
                                    if (defendCrit < 0) defendCrit = 0;
                                    enemyInfo.Crit = defendCrit;
                                    obj.text = defendCrit.ToString();
                                    break;
                                case "EnermyRepeat":
                                    if (enemy.Speed - PlayerUnits[currentUnitIndex].Speed >= 5)
                                    {
                                        obj.text = "X2";
                                        enemyInfo.Repeat = 2;
                                    }
                                    else
                                    {
                                        obj.text = "";
                                        enemyInfo.Repeat = 1;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        tilemap.RefreshAllTiles();
                        Debug.Log(123);
                        BtnAttackConfirmClick();
                        break;
                    }
                }
                if (hasPlayerUnit) break;
                tilemap.RefreshAllTiles();
                MovedUnitIndex.Add(currentEnemyIndex);
                SpriteRenderer spriteRender = EnemyUnits[currentEnemyIndex]._GameObject.GetComponent<SpriteRenderer>();
                spriteRender.color = Color.gray;
                currentEnemyIndex++;
                currentState = GameStates.ToEnemyTurn;
                break;
            case GameStates.AfterAnimationFight:

                break;    
            case GameStates.GameOver:
                if (!changeTurn)
                {
                    currentState = GameStates.AfterAnimationFight;
                }
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
