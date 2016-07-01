using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkMan : NetworkManager {
    public static bool Launch;
    private bool alreadyLaunched;
	// Use this for initialization
	void Start () {
        
    }
    public void OnLevelWasLoaded( int i )
    {
        alreadyLaunched = false;
        if (Launch)
        {
            Debug.Log( "StopServer" );
            StopServer();
        }
        else
        {
            Launch = true;
            GameObject.Find( "menu" ).SetActive( false );
        }
        networkAddress = AdventureBoard.HostAddress;
        networkPort = AdventureBoard.Port;
        Debug.Log( "coucou" );
        if (AdventureBoard.Online == "Offline")
        {
            Debug.Log( "server started" );
            StartServer();
        }
        else
        {
            if (AdventureBoard.HostState == "Meneur")
            {
                StartServer();
            }
            else
            {
                StartClient();
            }
        }
        AdventureBoard.Port++;
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
    }

    public void OnPlayerConnected()
    {
        Debug.Log("Player connected");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Client connected");

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Added player.");
        GameObject boardManager = GameObject.Find("BoardManager");
        bool assigned = boardManager.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        Debug.Log("Assigned authority = " + assigned);
        NetworkServer.Spawn(boardManager);
        //var player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
       // NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // Update is called once per frame
    void Update () {

        if (AdventureBoard.HostState != "Meneur" && client != null && client.connection != null && !alreadyLaunched)
        if (NetworkClient.active && !ClientScene.ready)
        {
            ClientScene.Ready( client.connection );


            ClientScene.AddPlayer( 0 );
                alreadyLaunched = true;
        }
    }
}
