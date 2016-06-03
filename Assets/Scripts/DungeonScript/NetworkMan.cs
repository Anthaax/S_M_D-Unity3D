using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkMan : NetworkManager {

	// Use this for initialization
	void Start () {
        string gameName = "Synouk_must_die";
        string roomName = "Synouk_room1";
        // MasterServer.ipAddress = "127.0.0.1";
        // Network.natFacilitatorIP = "127.0.0.1";
        // Network.InitializeServer(4, 25012, !Network.HavePublicAddress());
        // MasterServer.RegisterHost(gameName, roomName);
        Time.timeScale = 1.0f;
        GameObject.Find( "menu" ).SetActive( false );
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
	
	}
}
