using UnityEngine;
using System.Collections;
using S_M_D.Dungeon;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
//using System.Drawing;

public class BoardManager : NetworkBehaviour
{

    private Map map;
    private GameObject[,] goArray;
    private Transform boardHolder;

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject playerPrefab;
    public GameObject player;

    private bool online = true;
    private bool server;
    private bool mapInitialized = false;

    private Animator playerAnimator;
    public bool isActive { get; set; }

    public void Start()
    {
        if (!online)
            this.InitBoard();
        else
        {
            if (isServer) // We're a server so we just initialize the map
            {
                Debug.Log("Server initializing map");
                this.InitBoard();
            }
        }

        isActive = true;

    }

    public string mapToString()
    {
        string s = "";
        s += map.Width + '\n' + map.Height + '\n' + map.Rooms.Count + '\n';
        for (int i = 0; i < map.Rooms.Count; i++)
        {
            s += map.Rooms[i].ToString();
        }
        return s;
    }

    [Command]
    public void Cmd_AskServerForMap()
    {
        string s = "";
        s += map.Width.ToString() + '\n' + map.Height.ToString() + '\n' + map.Rooms.Count.ToString() + '\n';
        for (int i = 0; i < map.Rooms.Count; i++)
        {
            s += map.Rooms[i].ToString();
        }
        if (isServer)
            Rpc_SendClientsMap(s);
    }

    [ClientRpc]
    public void Rpc_MoveHero(string s_cells)
    {
        Debug.Log(s_cells);
        string[] cells = s_cells.Split('\n');
        List<Point> pts = new List<Point>();
        for (int i = 0; i < cells.Length - 1; i++)
        {
            string[] coords = cells[i].Split(' ');
            pts.Add(new Point(int.Parse(coords[0]), int.Parse(coords[1])));
        }
        MapItem r = map.Grid[pts[0].Y, pts[0].X];
        StartCoroutine(moveHero(pts, 0.1f, r));
    }

    [ClientRpc]
    public void Rpc_SendClientsMap(string s)
    {
        this.map = new Map(s);
        this.InitBoard(map);
    }

    public void InitBoard(Map map)
    {
        // boardHolder = new GameObject("Board").transform;
        Debug.Log("Calling other initboard !");
        this.map = map;
        this.goArray = new GameObject[map.Height, map.Width];

        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;

        float cellWidth = (camHalfWidth * 2) / map.Width;
        float cellHeight = (camHalfHeight * 2) / map.Height;

        player = Instantiate(playerPrefab, new Vector3(map.HeroPosition.X, map.HeroPosition.Y, 0f), Quaternion.identity) as GameObject;
        //player.AddComponent<BoxCollider2D>();
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                GameObject instance = new GameObject();
                GameObject toInstantiate = wallTiles[0];
                goArray[y, x] = instance;
                instance.name = "Cell|" + y.ToString() + "|" + x.ToString();
                // Adding box collider to game object
                //instance.AddComponent(typeof(BoxCollider2D));
                //toInstantiate.AddComponent(typeof(BoxCollider2D));

                if (map.Grid[y, x] == null)
                {
                    toInstantiate = wallTiles[0];
                }
                else if (map.Grid[y, x] == map.Rooms[0])
                {
                    toInstantiate = floorTiles[0];
                }
                else if (map.isNotVisited(map.Grid[y, x]))
                {
                    toInstantiate = floorTiles[1];
                }

                instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                //instance.AddComponent<BoxCollider2D>();

            }
        }
        Camera.main.orthographicSize /= 2;
        Camera.main.transform.position = new Vector3(map.HeroPosition.X, map.HeroPosition.Y, -10f);
        playerAnimator = player.GetComponent<Animator>();
    }

    // Use this for initialization
    public void InitBoard()
    {
        Debug.Log("Calling normal initboard !");
        boardHolder = new GameObject("Board").transform;

        this.map = new Map();
        Debug.Log("X = " + map.Rooms[0].Center.X + " Y = " + map.Rooms[0].Center.Y);
        this.goArray = new GameObject[map.Height, map.Width];

        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;

        float cellWidth = (camHalfWidth * 2) / map.Width;
        float cellHeight = (camHalfHeight * 2) / map.Height;

        player = Instantiate(playerPrefab, new Vector3(map.HeroPosition.X, map.HeroPosition.Y, 0f), Quaternion.identity) as GameObject;
        //player.AddComponent<BoxCollider2D>();
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                GameObject instance = new GameObject();
                GameObject toInstantiate = wallTiles[0];
                goArray[y, x] = instance;
                instance.name = "Cell|" + y.ToString() + "|" + x.ToString();
                // Adding box collider to game object
                //instance.AddComponent(typeof(BoxCollider2D));
                //toInstantiate.AddComponent(typeof(BoxCollider2D));

                if (map.Grid[y, x] == null)
                {
                    toInstantiate = wallTiles[0];
                }
                else if (map.Grid[y, x] == map.Rooms[0])
                {
                    toInstantiate = floorTiles[0];
                }
                else if (map.isNotVisited(map.Grid[y, x]))
                {
                    toInstantiate = floorTiles[1];
                }

                instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                //instance.AddComponent<BoxCollider2D>();

            }
        }
        Camera.main.orthographicSize /= 2;
        Camera.main.transform.position = new Vector3(map.HeroPosition.X, map.HeroPosition.Y, -10f);
        playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !isActive )
            return;
        if (!isServer && !mapInitialized)
        {
            Cmd_AskServerForMap();
            mapInitialized = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                string cellName = hit.transform.gameObject.name;
                int y = (int)hit.transform.gameObject.transform.position.y;
                int x = (int)hit.transform.gameObject.transform.position.x;
                MapItem room = map.Grid[y, x];
                Debug.Log(x + " " + y);
                if (room != null)
                {
                    if ((map.isVisited(room) || map.isNotVisited(room)))
                    {
                        StopAllCoroutines( );
                        List<Point> cellsToDest = map.leeAlgorithm(map.HeroPosition, new Point(x, y));
                        int i = 0;
                        string s = "";
                        for (i = 0; i < cellsToDest.Count; i++)
                        {
                            s += cellsToDest[i].X.ToString() + " " + cellsToDest[i].Y.ToString() + '\n';
                        }
                        Debug.Log(s);
                        Rpc_MoveHero(s);
                        StartCoroutine(moveHero(cellsToDest, 0.1f, room));

                    }
                }


            }
            else
                Debug.Log("No object was clicked");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ( map.HeroPosition.X + 1 < map.Width && map.Grid[ map.HeroPosition.Y, map.HeroPosition.X + 1] != null )
            {
                StartCoroutine( moveHeroToCell( new Point( map.HeroPosition.X + 1, map.HeroPosition.Y ), 0.1f ) );

            }               
        }
        else if ( Input.GetKeyDown( KeyCode.LeftArrow ) )
        {
            if ( map.HeroPosition.X - 1 >= 0 && map.Grid[ map.HeroPosition.Y, map.HeroPosition.X - 1 ] != null )
            {
                StartCoroutine( moveHeroToCell( new Point( map.HeroPosition.X - 1, map.HeroPosition.Y ), 0.1f ) );
            }
        }
        else if ( Input.GetKeyDown( KeyCode.UpArrow ) )
        {
            if ( map.HeroPosition.Y + 1 < map.Height && map.Grid[ map.HeroPosition.Y + 1, map.HeroPosition.X ] != null )
            {
                StartCoroutine( moveHeroToCell( new Point( map.HeroPosition.X , map.HeroPosition.Y + 1 ), 0.1f ) );
            }
        }
        else if ( Input.GetKeyDown( KeyCode.DownArrow ) )
        {
            if ( map.HeroPosition.Y - 1 >= 0 && map.Grid[ map.HeroPosition.Y - 1, map.HeroPosition.X  ] != null )
            {
                StartCoroutine( moveHeroToCell( new Point( map.HeroPosition.X , map.HeroPosition.Y - 1 ), 0.1f ) );
            }
        }
    }

    public IEnumerator moveHeroToCell( Point dest, float waitTime )
    {
        float diffX, diffY;
        diffX = ( dest.X - map.HeroPosition.X ) / 5f;
        diffY = ( dest.Y - map.HeroPosition.Y ) / 5f;
        if ( dest.X - map.HeroPosition.X > 0 )
            playerAnimator.SetInteger( "direction", 2 );
        else if ( dest.X - map.HeroPosition.X < 0 )
            playerAnimator.SetInteger( "direction", 1 );
        if ( dest.Y - map.HeroPosition.Y > 0 )
            playerAnimator.SetInteger( "direction", 3 );
        else if ( dest.Y - map.HeroPosition.Y < 0 )
            playerAnimator.SetInteger( "direction", 0 );
        for ( int i = 0; i <= 5; i++ )
        {

            player.transform.position = new Vector3( map.HeroPosition.X + diffX * i, map.HeroPosition.Y + diffY * i, 0f );
            Camera.main.transform.position = new Vector3( map.HeroPosition.X + diffX * i, map.HeroPosition.Y + diffY * i, -10f );
            yield return new WaitForSeconds( waitTime / 10f );
        }
        map.HeroPosition = dest;
        yield return new WaitForSeconds( waitTime );
        discoverRoom( map.Grid[ dest.Y, dest.X ] );
    }

    IEnumerator moveHero(List<Point> cellsToDest, float waitTime, MapItem room)
    {
        for (int k = cellsToDest.Count - 1; k >= 0; k--)
        {

            Point currentCell = cellsToDest[k];
            float diffX, diffY;
            diffX = (currentCell.X - map.HeroPosition.X) / 5f;
            diffY = (currentCell.Y - map.HeroPosition.Y) / 5f;
            if (currentCell.X - map.HeroPosition.X > 0)
                playerAnimator.SetInteger("direction", 2);
            else if (currentCell.X - map.HeroPosition.X < 0)
                playerAnimator.SetInteger("direction", 1);
            if (currentCell.Y - map.HeroPosition.Y > 0)
                playerAnimator.SetInteger("direction", 3);
            else if (currentCell.Y - map.HeroPosition.Y < 0)
                playerAnimator.SetInteger("direction", 0);
            for (int i = 0; i <= 5; i++)
            {

                player.transform.position = new Vector3(map.HeroPosition.X + diffX * i, map.HeroPosition.Y + diffY * i, 0f);
                Camera.main.transform.position = new Vector3(map.HeroPosition.X + diffX * i, map.HeroPosition.Y + diffY * i, -10f);
                yield return new WaitForSeconds(waitTime / 10f);
            }
            map.HeroPosition = currentCell;
            yield return new WaitForSeconds(waitTime);
            // Positionning camera to follow player
        }
        //Camera.main.transform.position = new Vector3(map.HeroPosition.X, map.HeroPosition.Y, -10f);

        discoverRoom(room);

        Debug.Log("inside moveHero");

    }

    IEnumerator moveHeroByKey(Vector3 end)
    {
        Rigidbody2D rb2D = player.GetComponent<Rigidbody2D>( );

        float sqrRemainingDistance = ( transform.position - end ).sqrMagnitude; 
        float moveTime = 0.1f;
        float inverseMoveTime = 1f / moveTime;

        while ( sqrRemainingDistance > float.Epsilon )
        {
            Vector3 newPosition = Vector3.MoveTowards( rb2D.position, end, inverseMoveTime * Time.deltaTime );
            rb2D.MovePosition( newPosition );
            sqrRemainingDistance = ( transform.position - end ).sqrMagnitude;
            yield return null;
        }
    }

    public void discoverRoom(MapItem room)
    {
        if (map.isNotVisited(room))
        {
            // adding room to visited rooms
            map.Visited.Add(room);
            // adding this neighbors to not visited
            map.addNeighborsToNotVisited(room);
            // removing room from notVisited 
            map.removeRoomFromNotVisited(room);

            Debug.Log("Number of not visited :" + map.NotVisited.Count);
            for (int cellY = 0; cellY < map.Height; cellY++)
            {
                for (int cellX = 0; cellX < map.Width; cellX++)
                {
                    if (map.Grid[cellY, cellX] == room)
                    {
                        goArray[cellY, cellX] = Instantiate(floorTiles[0], new Vector3(cellX, cellY, 0f), Quaternion.identity) as GameObject;
                    }
                    else if (map.isNotVisited(map.Grid[cellY, cellX]))
                    {
                        goArray[cellY, cellX] = Instantiate(floorTiles[1], new Vector3(cellX, cellY, 0f), Quaternion.identity) as GameObject;
                    }
                }
            }
        }
         foreach (MapItem i in map.NotVisited)
        {
            if (i is S_M_D.Dungeon.Room)
            {
                S_M_D.Dungeon.Room r = i as S_M_D.Dungeon.Room;
                foreach(string s in r.events)
                {
                    Debug.Log( s );
                }
                if (r.events.Contains("Chest"))
                {
                    Debug.Log( "Chest !" );
                    goArray[ r.Center.Y, r.Center.X ] = Instantiate( floorTiles[ 2 ], new Vector3( r.Center.X, r.Center.Y, 0f ), Quaternion.identity ) as GameObject;
                }
            }
        }
        if ( room is S_M_D.Dungeon.Room && ( ( S_M_D.Dungeon.Room ) room ).events.Contains( "Chest" ) )
        {
            string texturepath;
            Sprite inputSprite = null;

            GameObject canvas = GameObject.Find( "Canvas" );
            canvas.SetActive( true );
            GameObject menu = canvas.transform.Find( "menu" ).gameObject;
            menu.SetActive( true );
            Image img = GetComponent<Image>( );

            S_M_D.Dungeon.Room r = ( S_M_D.Dungeon.Room ) room;

            if ( r.chest[ 0 ] is S_M_D.Character.BaseArmor )
            {
                texturepath = "Assets/Sprites/Armor.png";

                inputSprite = ( Sprite ) AssetDatabase.LoadAssetAtPath( texturepath, typeof( Sprite ) );
                //menu.transform.Find( "ItemImage" ).GetComponent<Image>( ).sprite = Resources.Load( "Sprites/Armor" ) as Sprite;
            }
            else if ( r.chest[ 0 ] is S_M_D.Character.BaseWeapon )
            {
                texturepath = "Assets/Sprites/Weapon.png";

                inputSprite = ( Sprite ) AssetDatabase.LoadAssetAtPath( texturepath, typeof( Sprite ) );
                //menu.transform.Find( "ItemImage" ).GetComponent<Image>( ).sprite = Resources.Load( "Sprites/Weapon" ) as Sprite;
            }
            else if ( r.chest[ 0 ] is S_M_D.Character.BaseTrinket )
            {
                texturepath = "Assets/Sprites/Trinket.png";

                inputSprite = ( Sprite ) AssetDatabase.LoadAssetAtPath( texturepath, typeof( Sprite ) );
                //menu.transform.Find( "ItemImage" ).GetComponent<Image>( ).sprite = Resources.Load( "Sprites/Trinket" ) as Sprite;
            }

            menu.transform.Find( "ItemImage" ).GetComponent<Image>( ).sprite = inputSprite;
            menu.transform.Find( "ItemName" ).GetComponent<Text>( ).text = r.chest[0].ItemName;
            menu.transform.Find( "HP" ).GetComponent<Text>( ).text = r.chest[ 0 ].HP.ToString( );
            menu.transform.Find( "Mana" ).GetComponent<Text>( ).text = r.chest[ 0 ].Mana.ToString( );
            menu.transform.Find( "Defense" ).GetComponent<Text>( ).text = r.chest[ 0 ].Defense.ToString( );
            menu.transform.Find( "Damages" ).GetComponent<Text>( ).text = r.chest[ 0 ].Damage.ToString( );
            menu.transform.Find( "Speed" ).GetComponent<Text>( ).text = r.chest[ 0 ].Speed.ToString( );
            menu.transform.Find( "DodgeChance" ).GetComponent<Text>( ).text = r.chest[ 0 ].DodgeChance.ToString( );
            menu.transform.Find( "HitChance" ).GetComponent<Text>( ).text = r.chest[ 0 ].HitChance.ToString( );
            menu.transform.Find( "CritChance" ).GetComponent<Text>( ).text = r.chest[ 0 ].CritChance.ToString( );
            menu.transform.Find( "BleedingRes" ).GetComponent<Text>( ).text = r.chest[ 0 ].BleedingRes.ToString( );
            menu.transform.Find( "AffectRes" ).GetComponent<Text>( ).text = r.chest[ 0 ].AffectRes.ToString( );
            menu.transform.Find( "PoisonRes" ).GetComponent<Text>( ).text = r.chest[ 0 ].PoisonRes.ToString( );
            menu.transform.Find( "WaterRes" ).GetComponent<Text>( ).text = r.chest[ 0 ].WaterRes.ToString( );
            menu.transform.Find( "MagicRes" ).GetComponent<Text>( ).text = r.chest[ 0 ].MagicRes.ToString( );
            menu.transform.Find( "FireRes" ).GetComponent<Text>( ).text = r.chest[ 0 ].FireRes.ToString( );

            isActive = false;
            
        }

        if ( room is S_M_D.Dungeon.Room && ( ( S_M_D.Dungeon.Room ) room ).events.Contains( "Combat" ) )
        {
            SceneManager.LoadScene( "Combat" );
        }
        // setting non visited rooms images (monster or items room)
        /*
        for (int i = 0; i < map.NotVisited.Count; i++)
        {
            Room r = findRoomById(this.rooms, (int)this.notVisited[i]);
            switch (r.roomType)
            {
                case ROOM_MONSTERS:
                    this.goArray[r.getCenterY(), r.getCenterX()].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/skeleton", typeof(Sprite)) as Sprite;
                    Debug.Log("Monster room");
                    break;
                case ROOM_ITEMS:
                    this.goArray[r.getCenterY(), r.getCenterX()].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Treasure", typeof(Sprite)) as Sprite;
                    Debug.Log("Item room");
                    break;
            }
        }
        */
    }
}
