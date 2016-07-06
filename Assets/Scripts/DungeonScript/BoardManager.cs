using UnityEngine;
using System.Collections;
using S_M_D.Dungeon;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
//using UnityEditor;
using UnityEngine.SceneManagement;
using S_M_D.Character;
using S_M_D;
//using System.Drawing;

public class BoardManager : NetworkBehaviour
{

    public static Map Map;
    public static BaseHeros[] hero;
    public static GameContext Gtx;
    private GameObject[,] goArray;
    private Transform boardHolder;

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] miscTiles;
    public GameObject[] playerPrefab;
    public GameObject player1;
    public GameObject player2;
    private Point player2Position;

    private bool online = true;
    private bool server;
    private bool mapInitialized = false;

    private Animator playerAnimator;
    private Animator player2Animator;
    public bool isActive { get; set; }
    private bool heroesSent = false;

    private int updates = 0;
    public void Start( )
    {
        if (AdventureBoard.Online != "Online")
        {
            for (int i = 0; i < 4; i++)
                hero[i].Owner = "p1";
            this.InitBoard();
        }
        else
        {
            if (isServer) // We're a server so we just initialize the map
            {
                Debug.Log("Server initializing map");
                this.InitBoard();
            }
            else
            {

                this.getOtherPlayersHeroes();
            }
        }
        if (GameObject.Find("menu") != null)
        {
            GameObject.Find("menu").SetActive(false);
        }
        isActive = true;
    }
    public void getOtherPlayersHeroes( )
    {
        if ( isServer == true )
        {
            Debug.Log( "GetOtherPlayersHeroes isServer" );
            Rpc_AskClientToSendHeroes( );
        }
        else
        {
            Cmd_AskServerToSendHeroes( );
        }
    }

    [Command]
    public void Cmd_ServerCreateOtherPlayersHeroes(string className, string name, int isGuy, int idWeapon1, int idWeapon2, int idWeapon3, int idWeapon4, int level, int heroNb)
    {
        Debug.Log("Inside hero creation");
        Gtx.PlayerInfo.CreateHeroForMulti(className, name, isGuy, idWeapon1, idWeapon2, idWeapon3, idWeapon4, level);// Method
        hero[heroNb - 1] = Gtx.PlayerInfo.OnlineDude[heroNb - 3];
        if (heroNb == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log("Hero " + i + " name = " + hero[i].CharacterName);
            }
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                    hero[i].Owner = "p1";
                else
                    hero[i].Owner = "p2";
            }
        }

    }

    [ClientRpc]
    public void Rpc_ClientCreateOtherPlayersHeroes(string className, string name, int isGuy, int idWeapon1, int idWeapon2, int idWeapon3, int idWeapon4, int level, int heroNb)
    {
        Debug.Log("Inside hero creation");
        Gtx.PlayerInfo.CreateHeroForMulti(className, name, isGuy, idWeapon1, idWeapon2, idWeapon3, idWeapon4, level);// Method
        hero[heroNb - 1] = Gtx.PlayerInfo.OnlineDude[heroNb - 3];
        if (heroNb == 4)
        {
            BaseHeros hero0 = hero[0];
            BaseHeros hero1 = hero[1];
            BaseHeros hero2 = hero[2];
            BaseHeros hero3 = hero[3];
            hero[2] = hero0;
            hero[3] = hero1;
            hero[0] = hero2;
            hero[1] = hero3;
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                    hero[i].Owner = "p1";
                else
                    hero[i].Owner = "p2";
            }
        }
    }

    [ClientRpc]
    public void Rpc_AskClientToSendHeroes( )
    {
        Debug.Log( "AskClientToSendHeroes" );
        for (int i = 2; i < 4; i++)
        {
            int equip1Id, equip2Id, equip3Id, equip4Id;
            if ( hero[ i ].Equipement[ 0 ] == null ) equip1Id = 0; else equip1Id = hero[ i ].Equipement[ 0 ].ItemId;
            if ( hero[ i ].Equipement[ 1 ] == null ) equip2Id = 0; else equip2Id = hero[ i ].Equipement[ 1 ].ItemId;
            if ( hero[ i ].Equipement[ 2 ] == null ) equip3Id = 0; else equip3Id = hero[ i ].Equipement[ 2 ].ItemId;
            if ( hero[ i ].Equipement[ 3 ] == null ) equip4Id = 0; else equip4Id = hero[ i ].Equipement[ 3 ].ItemId;
            int isMale;
            if ( hero[ i ].IsMale ) isMale = 0; else isMale = 1;
            Cmd_ServerCreateOtherPlayersHeroes( hero[ i ].CharacterClassName, hero[ i ].CharacterName, isMale, equip1Id, equip2Id, equip3Id, equip4Id, hero[ i ].Lvl, 1 + i );
        }
    }

    [Command]
    public void Cmd_AskServerToSendHeroes( )
    {
        for ( int i = 0; i < 2; i++ )
        {
            int equip1Id, equip2Id, equip3Id, equip4Id;
            if ( hero[ i ].Equipement[ 0 ] == null ) equip1Id = 0; else equip1Id = hero[ i ].Equipement[ 0 ].ItemId;
            if ( hero[ i ].Equipement[ 1 ] == null ) equip2Id = 0; else equip2Id = hero[ i ].Equipement[ 1 ].ItemId;
            if ( hero[ i ].Equipement[ 2 ] == null ) equip3Id = 0; else equip3Id = hero[ i ].Equipement[ 2 ].ItemId;
            if ( hero[ i ].Equipement[ 3 ] == null ) equip4Id = 0; else equip4Id = hero[ i ].Equipement[ 3 ].ItemId;
            int isMale;
            if ( hero[ i ].IsMale ) isMale = 0; else isMale = 1;
            Rpc_ClientCreateOtherPlayersHeroes( hero[ i ].CharacterClassName, hero[ i ].CharacterName, isMale, equip1Id, equip2Id, equip3Id, equip4Id, hero[ i ].Lvl, 3 + i );
        }
    }
    public string mapToString()
    {
        string s = "";
        s += Map.Width + '\n' + Map.Height + '\n' + Map.Rooms.Count + '\n';
        for (int i = 0; i < Map.Rooms.Count; i++)
        {
            s += Map.Rooms[i].ToString();
        }
        return s;
    }

    [Command]
    public void Cmd_AskServerForMap()
    {
        string s = "";
        s += Map.Width.ToString() + '\n' + Map.Height.ToString() + '\n' + Map.Rooms.Count.ToString() + '\n';
        for (int i = 0; i < Map.Rooms.Count; i++)
        {
            s += Map.Rooms[i].ToString();
        }
        if (isServer)
            Rpc_SendClientsMap(s);
    }

    [ClientRpc]
    public void Rpc_MoveHero(string s_cells, string player)
    {
        Debug.Log(s_cells);
        string[] cells = s_cells.Split('\n');
        List<Point> pts = new List<Point>();
        for (int i = 0; i < cells.Length - 1; i++)
        {
            string[] coords = cells[i].Split(' ');
            pts.Add(new Point(int.Parse(coords[0]), int.Parse(coords[1])));
        }
        MapItem r = Map.Grid[pts[0].Y, pts[0].X];
        StartCoroutine(moveHero(player, pts, 0.1f, r, pts[0].X, pts[0].Y));
    }
    
    [Command]
    public void Cmd_SendServerHero2Position(int x, int y)
    {
        player2 = Instantiate( playerPrefab[ 1 ], new Vector3(x, y, 0), Quaternion.identity ) as GameObject;
        player2Animator = player2.GetComponent<Animator>();
    }

    [ClientRpc]
    public void Rpc_SendClientsMap(string s)
    {
        Map = new Map(Gtx, s);
        this.InitBoard(Map);
    }
    
    public void initMisc()
    {
        GameObject ironMaiden;
        ironMaiden = Instantiate( miscTiles[0], new Vector3( Map.Rooms[0].Center.X, Map.Rooms[ 0 ].Center.Y + 1, 0f ), Quaternion.identity ) as GameObject;
        ironMaiden.transform.localScale = new Vector3( 3f, 3f, 0f );
    }

    public void instantiatePlayers()
    {

        player1 = Instantiate( playerPrefab[0], new Vector3( Map.HeroPosition.X, Map.HeroPosition.Y, 0f ), Quaternion.identity ) as GameObject;
        Vector3 hero2Position = new Vector3( Map.HeroPosition.X, Map.HeroPosition.Y, 0.0f );
        if ( Map.HeroPosition.X - 1 >= 0 && Map.Grid[ Map.HeroPosition.Y, Map.HeroPosition.X - 1 ] != null )
            hero2Position = new Vector3( Map.HeroPosition.X - 1, Map.HeroPosition.Y , 0.0f);
        player2 = Instantiate( playerPrefab[ 1 ], hero2Position, Quaternion.identity ) as GameObject;
        Cmd_SendServerHero2Position(( int ) hero2Position.x, (int)hero2Position.y );
        player2Position = new Point((int)hero2Position.x, (int)hero2Position.y);
        player2Animator = player2.GetComponent<Animator>();
    }
    public void InitBoard(Map map)
    {
 
        // boardHolder = new GameObject("Board").transform;
        Debug.Log("Calling other initboard !");
        this.goArray = new GameObject[map.Height, map.Width];

        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;

        float cellWidth = (camHalfWidth * 2) / map.Width;
        float cellHeight = (camHalfHeight * 2) / map.Height;

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
        instantiatePlayers( );
        playerAnimator = player1.GetComponent<Animator>();
        initMisc( );
    }

    // Use this for initialization
    public void InitBoard()
    {
        Debug.Log("Calling normal initboard !");
        boardHolder = new GameObject("Board").transform;

        if ( Map == null )
            Debug.Log( "map null" );
        if ( Map.Rooms == null )
            Debug.Log( "rooms null" );
        if ( Map.Rooms[ 0 ] == null )
            Debug.Log( "room[0] null" );
        //if ( Map.Rooms[ 0 ].Center == null )
        //    Debug.Log( "room[0] center null" );
       

        Debug.Log("X = " + Map.Rooms[0].Center.X + " Y = " + Map.Rooms[0].Center.Y);
        this.goArray = new GameObject[Map.Height, Map.Width];

        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;

        float cellWidth = (camHalfWidth * 2) / Map.Width;
        float cellHeight = (camHalfHeight * 2) / Map.Height;

        player1 = Instantiate(playerPrefab[0], new Vector3(Map.HeroPosition.X, Map.HeroPosition.Y, 0f), Quaternion.identity) as GameObject;
        //player.AddComponent<BoxCollider2D>();
        for (int y = 0; y < Map.Height; y++)
        {
            for (int x = 0; x < Map.Width; x++)
            {
                GameObject instance = new GameObject();
                GameObject toInstantiate = wallTiles[0];
                goArray[y, x] = instance;
                instance.name = "Cell|" + y.ToString() + "|" + x.ToString();
                // Adding box collider to game object
                //instance.AddComponent(typeof(BoxCollider2D));
                //toInstantiate.AddComponent(typeof(BoxCollider2D));

                if (Map.Grid[y, x] == null)
                {
                    toInstantiate = wallTiles[0];
                }
                else if (Map.Grid[y, x] == Map.Rooms[0])
                {
                    toInstantiate = floorTiles[0];
                }
                else if (Map.isNotVisited(Map.Grid[y, x]))
                {
                    toInstantiate = floorTiles[1];
                }
                else if (Map.isVisited( Map.Grid[y, x] ))
                {
                    toInstantiate = floorTiles[0];
                }

                instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                //instance.AddComponent<BoxCollider2D>();

            }
        }
        Camera.main.orthographicSize /= 2;
        Camera.main.transform.position = new Vector3(Map.HeroPosition.X, Map.HeroPosition.Y, -10f);
        playerAnimator = player1.GetComponent<Animator>();
        initMisc( );
    }

    // Update is called once per frame
    void Update( )
    {
        if ( isServer && NetworkServer.connections.Count != 0 && heroesSent == false )
        {
            if (updates < 240)
                updates++;
            if (updates >= 240)
                updates = 240;
            if (updates == 240)
            {
                Debug.Log("Inside getOtherPlayersHeroes");
                this.getOtherPlayersHeroes();
                heroesSent = true;
            }
        }
        if ( !isActive )
            return;
        if ( !isServer && !mapInitialized && AdventureBoard.Online == "Online" && AdventureBoard.HostState == "Suiveur" )
        {
            Cmd_AskServerForMap( );
            mapInitialized = true;
        }

        if ( !isServer )
            return;
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            if ( Physics.Raycast( ray, out hit, float.MaxValue ) )
            {
                string cellName = hit.transform.gameObject.name;
                int y = ( int ) hit.transform.gameObject.transform.position.y;
                int x = ( int ) hit.transform.gameObject.transform.position.x;
                MapItem room = Map.Grid[ y, x ];
                Debug.Log( x + " " + y );
                if ( room != null )
                {
                    if ( ( Map.isVisited( room ) || Map.isNotVisited( room ) ) )
                    {
                        StopAllCoroutines( );
                        List<Point> cellsToDest = Map.leeAlgorithm( Map.HeroPosition, new Point( x, y ) );
                        int i = 0;
                        string s = "";
                        string s2 = "";
                        for ( i = 0; i < cellsToDest.Count; i++ )
                        {
                            s += cellsToDest[ i ].X.ToString( ) + " " + cellsToDest[ i ].Y.ToString( ) + '\n';
                            if ( i < cellsToDest.Count - 1 )
                                s2 += cellsToDest[ i ].X.ToString( ) + " " + cellsToDest[ i ].Y.ToString( ) + '\n'; ;
                        }
                        Debug.Log( s );
                        Rpc_MoveHero( s, "p1" );
                        StartCoroutine( moveHero( "p1", cellsToDest, 0.1f, room, cellsToDest[0].X, cellsToDest[0].Y) );
                        if ( player2 != null )
                        {
                            StartCoroutine( moveHero( "p2", cellsToDest, 0.1f, room, cellsToDest[0].X, cellsToDest[0].Y));
                            Rpc_MoveHero( s2, "p2" );
                        }
                    }
                }


            }
            else
                Debug.Log( "No object was clicked" );
        }
        else if ( Input.GetKey( KeyCode.RightArrow ) )
        {
            if ( Map.HeroPosition.X + 1 < Map.Width && Map.Grid[ Map.HeroPosition.Y, Map.HeroPosition.X + 1 ] != null )
            {
                Point p = new Point( Map.HeroPosition.X, Map.HeroPosition.Y );
                StartCoroutine( moveHeroToCell( new Point( Map.HeroPosition.X + 1, Map.HeroPosition.Y ), 0.1f, "p1" ) );
                //StartCoroutine( moveHeroByKey( new Vector3( Map.HeroPosition.X + 1, Map.HeroPosition.Y ), player1 ) );
                if ( player2 != null )
                {
                    Rpc_ActivateEventOnClient(p.X + 1, p.Y);
                    StartCoroutine( moveHeroToCell( new Point( p.X, p.Y ), 0.1f, "p2" ) );
                    Rpc_MoveHeroToCell( Map.HeroPosition.X + 1, Map.HeroPosition.Y, "p1" );
                    Rpc_MoveHeroToCell( p.X, p.Y, "p2" );
                }
            }
        }
        else if ( Input.GetKey( KeyCode.LeftArrow ) )
        {
            if ( Map.HeroPosition.X - 1 >= 0 && Map.Grid[ Map.HeroPosition.Y, Map.HeroPosition.X - 1 ] != null )
            {
                Point p = new Point( Map.HeroPosition.X, Map.HeroPosition.Y );
                StartCoroutine( moveHeroToCell( new Point( Map.HeroPosition.X - 1, Map.HeroPosition.Y ), 0.1f, "p1" ) );
                //StartCoroutine( moveHeroByKey( new Vector3( Map.HeroPosition.X - 1, Map.HeroPosition.Y ),player1 ) );
                if ( player2 != null )
                {
                    Rpc_ActivateEventOnClient(p.X - 1, p.Y);
                    StartCoroutine( moveHeroToCell( new Point( p.X, p.Y ), 0.1f, "p2" ) );
                    Rpc_MoveHeroToCell( Map.HeroPosition.X - 1, Map.HeroPosition.Y, "p1" );
                    Rpc_MoveHeroToCell( p.X, p.Y, "p2" );
                }
            }
        }
        else if ( Input.GetKey( KeyCode.UpArrow ) )
        {
            if ( Map.HeroPosition.Y + 1 < Map.Height && Map.Grid[ Map.HeroPosition.Y + 1, Map.HeroPosition.X ] != null )
            {
                Point p = new Point( Map.HeroPosition.X, Map.HeroPosition.Y );
                StartCoroutine( moveHeroToCell( new Point( Map.HeroPosition.X, Map.HeroPosition.Y + 1 ), 0.1f, "p1" ) );
                //StartCoroutine( moveHeroByKey( new Vector3( Map.HeroPosition.X, Map.HeroPosition.Y + 1 ), player1 ) );
                if ( player2 != null )
                {
                    Rpc_ActivateEventOnClient(p.X, p.Y + 1);
                    StartCoroutine( moveHeroToCell( new Point( p.X, p.Y ), 0.1f, "p2" ) );
                    Rpc_MoveHeroToCell( Map.HeroPosition.X, Map.HeroPosition.Y + 1, "p1" );
                    Rpc_MoveHeroToCell( p.X, p.Y, "p2" );
                }
            }
        }
        else if ( Input.GetKey( KeyCode.DownArrow ) )
        {
            if ( Map.HeroPosition.Y - 1 >= 0 && Map.Grid[ Map.HeroPosition.Y - 1, Map.HeroPosition.X ] != null )
            {
                Point p = new Point( Map.HeroPosition.X, Map.HeroPosition.Y );
                StartCoroutine( moveHeroToCell( new Point( Map.HeroPosition.X, Map.HeroPosition.Y - 1 ), 0.1f, "p1" ) );
                //StartCoroutine( moveHeroByKey( new Vector3( Map.HeroPosition.X, Map.HeroPosition.Y - 1 ), player1 ) );
                if ( player2 != null )
                {
                    Rpc_ActivateEventOnClient(p.X, p.Y - 1);
                    StartCoroutine( moveHeroToCell( new Point( p.X, p.Y ), 0.1f, "p2" ) );
                    Rpc_MoveHeroToCell( Map.HeroPosition.X, Map.HeroPosition.Y - 1, "p1" );
                    Rpc_MoveHeroToCell( p.X, p.Y, "p2" );
                }
            }
        }
    }


    public IEnumerator moveHeroToCell( Point dest, float waitTime, string player )
    {
        Point heroPos;
        Animator heroAnimator;
        GameObject playerGo;
        if ( player == "p1" )
        {
            heroPos = Map.HeroPosition;
            heroAnimator = playerAnimator;
            playerGo = player1;
        }
        else
        {
            heroPos = player2Position;
            heroAnimator = player2Animator;
            playerGo = player2;
        }
        float diffX, diffY;
        diffX = ( dest.X - heroPos.X ) / 5f;
        diffY = ( dest.Y - heroPos.Y ) / 5f;

        if ( dest.X - heroPos.X > 0 )
            heroAnimator.SetInteger( "direction", 2 );
        else if ( dest.X - heroPos.X < 0 )
            heroAnimator.SetInteger( "direction", 1 );
        if ( dest.Y - heroPos.Y > 0 )
            heroAnimator.SetInteger( "direction", 3 );
        else if ( dest.Y - heroPos.Y < 0 )
            heroAnimator.SetInteger( "direction", 0 );
        for ( int i = 0; i <= 5; i++ )
        {
            float step = 0.2f * Time.deltaTime;
            playerGo.transform.position = Vector3.MoveTowards( new Vector3( heroPos.X, heroPos.Y, 0f ), new Vector3( dest.X, dest.Y, 0f ), step );
            Camera.main.transform.position = Vector3.MoveTowards( new Vector3( heroPos.X, heroPos.Y, -10f ), new Vector3( dest.X, dest.Y, -10f ), step );

            yield return new WaitForSeconds( waitTime / 10f );
        }
        playerGo.transform.position = new Vector3( dest.X, dest.Y );
        if ( player == "p1" )
            Map.HeroPosition = dest;
        else
            player2Position = dest;
        yield return new WaitForSeconds( waitTime );
        discoverRoom( Map.Grid[ dest.Y, dest.X ], dest.X, dest.Y );
    }

    public IEnumerator moveHeroToCell( Point dest, float waitTime )
    {
        float diffX, diffY;
        diffX = (float)( dest.X - Map.HeroPosition.X ) / 5f;
        diffY = ( float ) ( dest.Y - Map.HeroPosition.Y ) / 5f;
        if ( dest.X - Map.HeroPosition.X > 0 )
            playerAnimator.SetInteger( "direction", 2 );
        else if ( dest.X - Map.HeroPosition.X < 0 )
            playerAnimator.SetInteger( "direction", 1 );
        if ( dest.Y - Map.HeroPosition.Y > 0 )
            playerAnimator.SetInteger( "direction", 3 );
        else if ( dest.Y - Map.HeroPosition.Y < 0 )
            playerAnimator.SetInteger( "direction", 0 );
        for ( int i = 0; i < 5; i++ )
        {

            //player1.transform.position = new Vector3( map.HeroPosition.X + diffX * i, map.HeroPosition.Y + diffY * i, 0f );
            //Camera.main.transform.position = new Vector3( map.HeroPosition.X + diffX * i, map.HeroPosition.Y + diffY * i, -10f );

            float step = 0.2f * Time.deltaTime;
            //player1.transform.position = Vector3.MoveTowards( new Vector3( Map.HeroPosition.X, Map.HeroPosition.Y, 0f ), new Vector3( Map.HeroPosition.X + diffX * i, Map.HeroPosition.Y + diffY * i, 0f ), step );
            player1.transform.position = Vector3.MoveTowards( new Vector3( Map.HeroPosition.X + diffX * i, Map.HeroPosition.Y + diffY * i, 0f ), new Vector3( Map.HeroPosition.X + diffX * (i + 1), Map.HeroPosition.Y + diffY * (i + 1), 10f ),1 );

            Camera.main.transform.position = Vector3.MoveTowards( new Vector3( Map.HeroPosition.X,Map.HeroPosition.Y,-10f ),new Vector3( Map.HeroPosition.X + diffX * i, Map.HeroPosition.Y + diffY * i, -10f ),1);

            yield return new WaitForSeconds( waitTime / 10f );
        }
        player1.transform.position = new Vector3( dest.X, dest.Y );
        Map.HeroPosition = dest;
        yield return new WaitForSeconds( waitTime );
        discoverRoom( Map.Grid[ dest.Y, dest.X ], dest.X, dest.Y );
    }

    IEnumerator moveHero(string player, List<Point> cellsToDest, float waitTime, MapItem room, int x, int y)
    {
        Animator anim;
        Point heroPosition;
        if (player == "p1")
        {
            anim = playerAnimator;
            heroPosition = Map.HeroPosition;
        }
        else
        {
            anim = player2Animator;
            heroPosition = player2Position;
        }
        for (int k = cellsToDest.Count - 1; k >= 0; k--)
        {
            Point currentCell = cellsToDest[k];
            float diffX, diffY;
            diffX = (currentCell.X - Map.HeroPosition.X) / 5f;
            diffY = (currentCell.Y - Map.HeroPosition.Y) / 5f;
            if (currentCell.X - heroPosition.X > 0)
                anim.SetInteger("direction", 2);
            else if (currentCell.X - heroPosition.X < 0)
                anim.SetInteger("direction", 1);
            if (currentCell.Y - heroPosition.Y > 0)
                anim.SetInteger("direction", 3);
            else if (currentCell.Y - heroPosition.Y < 0)
               anim.SetInteger("direction", 0);
            for (int i = 0; i <= 5; i++)
            {
                if (player == "p1")
                    player1.transform.position = new Vector3(heroPosition.X + diffX * i, heroPosition.Y + diffY * i, 0f);
                else if (player == "p2")
                    player2.transform.position = new Vector3(heroPosition.X + diffX * i, heroPosition.Y + diffY * i, 0f);
                Camera.main.transform.position = new Vector3(Map.HeroPosition.X + diffX * i, Map.HeroPosition.Y + diffY * i, -10f);
                yield return new WaitForSeconds(waitTime / 10f);
            }
            if (player == "p1")
                Map.HeroPosition = currentCell;
            else if (player == "p2")
                player2Position = currentCell;
            heroPosition = currentCell;
            yield return new WaitForSeconds(waitTime);
            // Positionning camera to follow player
        }
        //Camera.main.transform.position = new Vector3(map.HeroPosition.X, map.HeroPosition.Y, -10f);

        discoverRoom(room, x, y);

        Debug.Log("inside moveHero");

    }

    IEnumerator moveHeroByKey(Vector3 end, GameObject player)
    {
        //Rigidbody2D rb2D = player.GetComponent<Rigidbody2D>( );

        //float sqrRemainingDistance = ( transform.position - end ).sqrMagnitude; 
        //float moveTime = 0.1f;
        //float inverseMoveTime = 1f / moveTime;

        //while ( sqrRemainingDistance > float.Epsilon )
        //{
        //    Vector3 newPosition = Vector3.MoveTowards( rb2D.position, end, inverseMoveTime * Time.deltaTime );
        //    rb2D.MovePosition( newPosition );
        //    sqrRemainingDistance = ( transform.position - end ).sqrMagnitude;
        //    yield return null;
        //    break;
        //}
        //Camera.main.transform.position = new Vector3( end.x, end.y, -10f );
        //discoverRoom( Map.Grid[(int)end.y,(int)end.x]);
        float hSpeed = Input.GetAxis( "Horizontal" );
        float vSpeed = Input.GetAxis( "Vertical" );
        float Speed = 8.0f;

        if ( hSpeed > 0 )
        {
            transform.localScale = new Vector3( -1, 1, 1 );
            //m_Animator.Play( "LinkWalkLeft" );
            //direction = "Left";

        }

        else if ( hSpeed < 0 )
        {
            transform.localScale = new Vector3( 1, 1, 1 );
            //m_Animator.Play( "LinkWalkLeft" );
            //direction = "Left";
        }


        else if ( vSpeed > 0 )
        {
            transform.localScale = new Vector3( -1, 1, 1 );
            //m_Animator.Play( "LinkWalkUp" );
            //direction = "Up";
        }
        else if ( vSpeed < 0 )
        {
            transform.localScale = new Vector3( 1, 1, 1 );
            //m_Animator.Play( "LinkWalkDown" );
            //direction = "Down";
        }

        player.GetComponent<Rigidbody2D>( ).velocity = new Vector2( hSpeed * Speed, player.GetComponent<Rigidbody2D>( ).velocity.y );
        player.GetComponent<Rigidbody2D>( ).velocity = new Vector2( player.GetComponent<Rigidbody2D>( ).velocity.x, vSpeed * Speed );

        yield return null;
    }

    [ClientRpc]
    public void Rpc_ActivateEventOnClient(int x, int y)
    {
        discoverRoom(Map.Grid[y, x], x, y);
    }

    [ClientRpc]
    public void Rpc_MoveHeroToCell( int x, int y, string player )
    {
        Debug.Log( "Moving hero on client" );
        StartCoroutine( moveHeroToCell( new Point( x, y ), 0.1f, player ) );
    }

    public void discoverRoom(MapItem room, int x, int y)
    {
        if (Map.isNotVisited(room))
        {
            // adding room to visited rooms
            Map.Visited.Add(room);
            // adding this neighbors to not visited
            Map.addNeighborsToNotVisited(room);
            // removing room from notVisited 
            Map.removeRoomFromNotVisited(room);

            Debug.Log("Number of not visited :" + Map.NotVisited.Count);
            for (int cellY = 0; cellY < Map.Height; cellY++)
            {
                for (int cellX = 0; cellX < Map.Width; cellX++)
                {
                    if (Map.Grid[cellY, cellX] == room)
                    {
                        goArray[cellY, cellX] = Instantiate(floorTiles[0], new Vector3(cellX, cellY, 0f), Quaternion.identity) as GameObject;
                    }
                    else if (Map.isNotVisited(Map.Grid[cellY, cellX]))
                    {
                        goArray[cellY, cellX] = Instantiate(floorTiles[1], new Vector3(cellX, cellY, 0f), Quaternion.identity) as GameObject;
                    }
                }
            }
        }
         foreach (MapItem i in Map.NotVisited)
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
                else if (r.events.Contains("Combat"))
                {
                    r.events.Remove( "Combat" );
                    StartCombat.Gtx = Gtx;
                    StartCombat.Heros = hero;
                    StartCombat.Map = Map;
                    if (GameObject.Find("NetworkManager") != null)
                        Destroy(GameObject.Find("NetworkManager").gameObject);
                    //Network.Disconnect();
                    SceneManager.LoadScene( 3 );
                    //GameObject.Find( "NetworkManager" ).GetComponent<NetworkMan>().StopServer();
                    
                }
            }
        }
        if ( room is S_M_D.Dungeon.Room && ( ( S_M_D.Dungeon.Room ) room ).events.Contains( "Chest" ) )
        {
            Sprite inputSprite = null;

            GameObject canvas = GameObject.Find( "Canvas" );
            canvas.SetActive( true );
            GameObject menu = canvas.transform.Find( "menu" ).gameObject;
            menu.SetActive( true );
            Image img = GetComponent<Image>( );

            S_M_D.Dungeon.Room r = ( S_M_D.Dungeon.Room ) room;

            if ( r.chest[ 0 ] is S_M_D.Character.BaseArmor )
            {
                inputSprite = Resources.Load<Sprite>( "Sprites/Dungeon/Armor" );
            }
            else if ( r.chest[ 0 ] is S_M_D.Character.BaseWeapon )
            {
                inputSprite = Resources.Load<Sprite>( "Sprites/Dungeon/Weapon" );
            }
            else if ( r.chest[ 0 ] is S_M_D.Character.BaseTrinket )
            {
                inputSprite = Resources.Load<Sprite>( "Sprites/Dungeon/Trinket" );
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
            r.events.Remove( "Chest" );

        }

    }
}
